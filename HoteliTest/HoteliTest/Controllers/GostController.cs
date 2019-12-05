using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HoteliTest.DAL;
using HoteliTest.Models;
using NLog;
using PagedList;

namespace HoteliTest.Controllers
{
    public class GostController : BaseController
    {
        

        public GostController() { }

        public GostController(IHotelAC context) 
        {
            db = context;
        }

        // GET: Gost
        public ActionResult Index(string sortOrder,string currentFilter,string searchString, int? page)
        {
            ViewBag.TrenutniSort = sortOrder;
            ViewBag.ImeSortParm = String.IsNullOrEmpty(sortOrder) ? "ime_desc" : "";
           

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var gosti = from s in db.Gosti
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                gosti = gosti.Where(s => s.Ime.Contains(searchString) || s.Prezime.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ime_desc":
                    gosti = gosti.OrderByDescending(s => s.Ime);
                    break;
             
                default:
                    gosti = gosti.OrderBy(s => s.Ime);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(gosti.ToPagedList(pageNumber,pageSize));
        }

        // GET: Gost/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gost gost = db.Gosti.Find(id);
            if (gost == null)
            {
                return HttpNotFound();
            }
            return View(gost);
        }

        // GET: Gost/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GostID,Ime,Prezime,Email,Adresa")] Gost gost)
        {
           
                if (ModelState.IsValid)
                {
                    db.Gosti.Add(gost);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
           
            return View(gost);
        }

        // GET: Gost/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gost gost = db.Gosti.Find(id);
            if (gost == null)
            {
                return HttpNotFound();
            }
            return View(gost);
        }

        // POST: Gost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GostID,Ime,Prezime,Email,Adresa")] Gost gost)
        {
            if (ModelState.IsValid)
            {
                db.ChangeState<Gost>(gost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gost);
        }

        /*[HttpPost,ActionName("Edit")]
    [ValidateAntiForgeryToken]
    public ActionResult EditPost(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        var gostToUpdate = db.Gosti.Find(id);
        if (TryUpdateModel(gostToUpdate, "", new string[] { "Ime", "Prezime", "Email","Adresa" }))
        {

                db.SaveChanges();
                return RedirectToAction("Index");

        }
        return View(gostToUpdate);

    }*/

        // GET: Gost/Delete/5
        public ActionResult Delete(int? id)
        {
           
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Gost gost = db.Gosti.Find(id);
                if (gost == null)
                {
                    return HttpNotFound();
                }
                return View(gost);
           
        }

        // POST: Gost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
                Gost gost = db.Gosti.Find(id);
                db.Gosti.Remove(gost);
                db.SaveChanges();
                return RedirectToAction("Index");
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
