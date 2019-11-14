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
using NLog;

namespace HoteliTest.Controllers
{
    public class UslugaController : Controller
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private IHotelAC db = new HotelContext();

        public UslugaController() { }

        public UslugaController(IHotelAC context)
        {
            db = context;
        }

        // GET: Usluga
        public ActionResult Index()
        {
            var usluge = db.Usluge.Include(r => r.StavkaRacuna);
            return View(db.Usluge.ToList());
        }

        // GET: Usluga/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usluga usluga = db.Usluge.Find(id);
            if (usluga == null)
            {
                return HttpNotFound();
            }
            return View(usluga);
        }

        // GET: Usluga/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usluga/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UslugaID,CijenaUsluge,ImeUsluge")] Usluga usluga)
        {
            if (ModelState.IsValid)
            {
                db.Usluge.Add(usluga);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usluga);
        }

        // GET: Usluga/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usluga usluga = db.Usluge.Find(id);
            if (usluga == null)
            {
                return HttpNotFound();
            }
            return View(usluga);
        }

        // POST: Usluga/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var uslugaToUpdate = db.Usluge.Find(id);
            if (TryUpdateModel(uslugaToUpdate, "", new string[] { "Ime", "Prezime", "Email", "Adresa" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return View("Error", new HandleErrorInfo(ex, "Usluga", "EditPost"));
                }
            }
            return View(uslugaToUpdate);
        }

        // GET: Usluga/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usluga usluga = db.Usluge.Find(id);
            if (usluga == null)
            {
                return HttpNotFound();
            }
            return View(usluga);
        }

        // POST: Usluga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usluga usluga = db.Usluge.Find(id);
            db.Usluge.Remove(usluga);
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
