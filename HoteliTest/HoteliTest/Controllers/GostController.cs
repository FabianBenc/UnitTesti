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
        public ViewResult Index()
        {
            return View("Index", db.Gosti.ToList());
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
                //db.ChangeState<Gost>(gost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gost);
        }

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
