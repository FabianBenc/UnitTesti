using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HoteliTest.DAL;
using HoteliTest.Models;

namespace HoteliTest.Controllers
{
    public class SobaController : BaseController
    {
       

        public SobaController() { }

        public SobaController(IHotelAC context)
        {
            db = context;
        }

        // GET: Soba
        public ActionResult Index()
        {
            var sobe = db.Sobe.Include(s => s.Hotel).Include(s => s.TipSobe);
            return View(sobe.ToList());
        }

        // GET: Soba/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soba soba = db.Sobe.Find(id);
            if (soba == null)
            {
                return HttpNotFound();
            }
            return View(soba);
        }

        // GET: Soba/Create
        public ActionResult Create()
        {
            ViewBag.HotelID = new SelectList(db.Hoteli, "HotelID", "Ime");
            ViewBag.TipSobeID = new SelectList(db.TipSoba, "TipSobeID", "OpisSobe");
            return View();
        }

        // POST: Soba/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SobaID,HotelID,BrojSobe,TipSobeID")] Soba soba)
        {
            if (ModelState.IsValid)
            {

                db.Sobe.Add(soba);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HotelID = new SelectList(db.Hoteli, "HotelID", "Ime", soba.HotelID);
            ViewBag.TipSobeID = new SelectList(db.TipSoba, "TipSobeID", "OpisSobe", soba.TipSobeID);
            return View(soba);
        }

        // GET: Soba/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soba soba = db.Sobe.Find(id);
            if (soba == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelID = new SelectList(db.Hoteli, "HotelID", "Ime", soba.HotelID);
            ViewBag.TipSobeID = new SelectList(db.TipSoba, "TipSobeID", "OpisSobe", soba.TipSobeID);
            return View(soba);
        }

        // POST: Soba/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sobaToUpdate = db.Sobe.Find(id);
            if (TryUpdateModel(sobaToUpdate, "", new string[] { "HotelID", "BrojSobe", "TipSobeID"}))
            {
                
                    db.SaveChanges();
                    return RedirectToAction("Index");
                
            }
            return View(sobaToUpdate);

        }

        //hotelid ime tip sobe id opis sobe

        // GET: Soba/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soba soba = db.Sobe.Find(id);
            if (soba == null)
            {
                return HttpNotFound();
            }
            return View(soba);
        }

        // POST: Soba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Soba soba = db.Sobe.Find(id);
            db.Sobe.Remove(soba);
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
