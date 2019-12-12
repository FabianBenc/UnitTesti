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
    public class StavkaRacunaController : BaseController
    {
       

        public StavkaRacunaController() { }

        public StavkaRacunaController(IHotelAC context)
        {
            db = context;
        }

        // GET: StavkaRacuna
        public ActionResult Index()
        {
            //var stavkeRacuna = db.StavkeRacuna.Include(s => s.Racun).Include(s => s.Usluga);
            return View(db.StavkeRacuna.ToList());
        }

        // GET: StavkaRacuna/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaRacuna stavkaRacuna = db.StavkeRacuna.Find(id);
            if (stavkaRacuna == null)
            {
                return HttpNotFound();
            }
            return View(stavkaRacuna);
        }

        // GET: StavkaRacuna/Create
        public ActionResult Create()
        {
            
                ViewBag.UslugaID = new SelectList(db.Usluge, "UslugaID", "ImeUsluge");
                ViewBag.RacunID = new SelectList(db.Racuni, "RacunID", "RacunID");

                return View();
            
        }

        // POST: StavkaRacuna/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StavkaRacunaID,Kolicina")] StavkaRacuna stavkaRacuna, int ID, Usluga usluga)
        {
            

                if (ModelState.IsValid)
                {
                    StavkaRacuna stavkaRacuna1 = new StavkaRacuna
                    {
                        Kolicina = stavkaRacuna.Kolicina,
                        RacunID = ID,
                        UslugaID = usluga.UslugaID,
                    };

                    double ukupnaCijenaUsluge = stavkaRacuna.Kolicina * db.Usluge.First(r => r.UslugaID == stavkaRacuna1.UslugaID).CijenaUsluge;

                    db.Racuni.First(r => r.RacunID == stavkaRacuna1.RacunID).IznosUkupno += ukupnaCijenaUsluge;

                    db.StavkeRacuna.Add(stavkaRacuna1);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            
            ViewBag.UslugaID = new SelectList(db.Usluge, "UslugaID", "ImeUsluge", stavkaRacuna.UslugaID);
            ViewBag.RacunID = new SelectList(db.Racuni, "RacunID", "RacunID", stavkaRacuna.RacunID);
           
            return View(stavkaRacuna);
        }

        // GET: StavkaRacuna/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaRacuna stavkaRacuna = db.StavkeRacuna.Find(id);
            if (stavkaRacuna == null)
            {
                return HttpNotFound();
            }
            
            return View(stavkaRacuna);
        }

        // POST: StavkaRacuna/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StavkaRacunaID,Kolicina,RacunID,UslugaID")] StavkaRacuna stavkaRacuna)
        {
            if (ModelState.IsValid)
            {
                //db.ChangeState<StavkaRacuna>(stavkaRacuna);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(stavkaRacuna);
        }

        // GET: StavkaRacuna/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StavkaRacuna stavkaRacuna = db.StavkeRacuna.Find(id);
            if (stavkaRacuna == null)
            {
                return HttpNotFound();
            }
            return View(stavkaRacuna);
        }

        // POST: StavkaRacuna/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StavkaRacuna stavkaRacuna = db.StavkeRacuna.Find(id);
            db.StavkeRacuna.Remove(stavkaRacuna);
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
