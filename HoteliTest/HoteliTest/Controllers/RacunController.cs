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
using HoteliTest.ViewModels;
using NLog;

namespace HoteliTest.Controllers
{
    public class RacunController : BaseController
    {
       

        public RacunController() { }

        public RacunController(IHotelAC context)
        {
            db = context;
        }

        // GET: Racun
        public ActionResult Index()
        {
            
                var racuni = db.Racuni.Include(r => r.Rezervacija);
                return View(racuni.ToList());
            
        }

        // GET: Racun/Details/5
        public ActionResult Details(int? id)
        {
            var usluga = db.StavkeRacuna.Where(v => v.RacunID == id).Include(v => v.Usluga);
            return View(usluga);
        }

        // GET: Racun/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Racun/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RacunID,Placeno,IznosUkupno,IznosDuga,RezervacijaID")] Racun racun, Rezervacija rezervacija, int id)
        {
           
                if (ModelState.IsValid)
                {
                    var racuni = new Racun
                    {
                        IznosUkupno = 0,
                        RacunID = racun.RacunID,
                        RezervacijaID = id
                    };

                    db.Racuni.Add(racuni);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Racun", racun.RacunID);
                }
                return View(racun);
           
           

            
        }
        [HttpGet]
        // GET: Racun/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuni.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            //ViewBag.RezervacijaID = new SelectList(db.Rezervacije, "RezervacijaID", "RezervacijaID", racun.RezervacijaID);
            return View(racun);
        }

        // POST: Racun/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Prezime, Placeno, IznosUkupno, RacunID")] Racun receipt)
        {
                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.RezervacijaID =
                new SelectList(db.Rezervacije, "RezervacijaID", "RezervacijaID", receipt.RezervacijaID);
                return View(receipt);
            
        }

        // GET: Racun/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Racun racun = db.Racuni.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(racun);
        }

        // POST: Racun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Racun racun = db.Racuni.Find(id);
            db.Racuni.Remove(racun);
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

        public ActionResult Odlazak(int? id, int? racunID, int? stavkaRacunaID, UslugeViewModel viewModel)
        {
            
                StavkaRacuna stavkaRacuna = db.StavkeRacuna.Find(stavkaRacunaID);
                Rezervacija rezervacija = db.Rezervacije.Find(id);
                Racun racun = db.Racuni.Find(racunID);

                var daniBoravka = (rezervacija.Odjava - rezervacija.Prijava).TotalDays;
                var popust = rezervacija.Popust;
                var ukupnaCijenaRezervacije = (double?)rezervacija.Soba.TipSobe.CijenaPoNoci;

                if (daniBoravka <= 1)
                {
                    ukupnaCijenaRezervacije += 100;
                }
                if (daniBoravka > 1)
                {
                    ukupnaCijenaRezervacije *= (daniBoravka);
                }
                if (popust > 0)
                {
                    ukupnaCijenaRezervacije -= ukupnaCijenaRezervacije * (double)(popust / 100);
                }

                UslugeViewModel uslugeView = new UslugeViewModel
                {
                    CijenaRezervacije = (decimal)ukupnaCijenaRezervacije,
                    Prijava = rezervacija.Prijava,
                    Odjava = rezervacija.Odjava,
                    Prezime = rezervacija.Gost.Prezime,
                    RezervacijaID = rezervacija.RezervacijaID,
                    Popust = rezervacija.Popust

                };

                var racuni = new Racun
                {
                    IznosUkupno = (double)uslugeView.CijenaRezervacije,
                    RezervacijaID = uslugeView.RezervacijaID,
                    RacunID = uslugeView.RacunID
                };
                db.Racuni.Add(racuni);
                db.SaveChanges();

                return View(uslugeView);
           
        }
    }
}
