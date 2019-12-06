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

namespace HoteliTest.Controllers
{
    public class HotelController : BaseController
    {
       

        public HotelController() { }

        public HotelController(IHotelAC context)
        {
            db = context;
        }

        // GET: Hotel
        public ActionResult Index()
        {
            return View(db.Hoteli.ToList());
        }

        // GET: Hotel/Details/5
        public ActionResult Details(int? id)
        {
           
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Hotel hotel = db.Hoteli.Find(id);
                if (hotel == null)
                {
                    return HttpNotFound();
                }
                return View(hotel);
           
        }

        // GET: Hotel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hotel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HotelID,Ime,Adresa,Lokacija")] Hotel hotel)
        {
            
                if (ModelState.IsValid)
                {
                    db.Hoteli.Add(hotel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(hotel);
           
        }

        // GET: Hotel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hoteli.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HotelID,Ime,Lokacija,Adresa")]Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.ChangeState<Hotel>(hotel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel);
        }
        
        // GET: Hotel/Delete/5
        public ActionResult Delete(int? id)
        {
           
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Hotel hotel = db.Hoteli.Find(id);
                if (hotel == null)
                {
                    return HttpNotFound();
                }
                return View(hotel);
           
        }

        // POST: Hotel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hotel hotel = db.Hoteli.Find(id);
            db.Hoteli.Remove(hotel);
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
