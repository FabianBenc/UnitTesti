using HoteliTest.DAL;
using HoteliTest.Models;
using HoteliTest.ViewModels;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data;
using Itenso.TimePeriod;
using NLog;

namespace HoteliTest.Controllers
{
    public class RezervacijaController : Controller
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private IHotelAC db = new HotelContext();

        public RezervacijaController() { }

        public RezervacijaController(IHotelAC context)
        {
            db = context;
        }

        // GET: Rezervacija
        public ActionResult Index()
        {
            var rezervacije = db.Rezervacije.Include(r => r.Gost).Include(r => r.Soba);
            return View(rezervacije.ToList());
        }

        // GET: Rezervacija/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacije.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            return View(rezervacija);
        }

        // GET: Rezervacija/Create
        public ActionResult Create(RezervacijaView rezervacijaView)
        {
            try
            {
                if (rezervacijaView == null)
                {
                    rezervacijaView.Popust = 0;
                }

               if (ModelState.IsValid)
               {
                    Rezervacija rezervacije = new Rezervacija
                    {
                        RezervacijaID = rezervacijaView.RezervacijaID,
                        SobaID = rezervacijaView.SobaID,
                        Rezervirano = rezervacijaView.Rezervirano,
                        Popust = rezervacijaView.Popust ?? 0,
                        GostID = rezervacijaView.GostID,
                        Prijava = rezervacijaView.Prijava,
                        Odjava = rezervacijaView.Odjava,
                    };

                    db.Rezervacije.Add(rezervacije);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                   
               }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return View("Error", new HandleErrorInfo(ex, "Rezervacija", "Create"));

            }
            return RedirectToAction("Index");
        }

        // GET: Rezervacija/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacije.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            return View(rezervacija);
        }

        // POST: Rezervacija/Edit/5
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
            var rezervacijaToUpdate = db.Rezervacije.Find(id);
            if (TryUpdateModel(rezervacijaToUpdate, "", new string[] { "Prezime", "Rezervirano", "Prijava","Odjava" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return View("Error", new HandleErrorInfo(ex, "Rezervacija", "EditPost"));
                }
            }
            return View(rezervacijaToUpdate);
        }

        // GET: Rezervacija/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacije.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            return View(rezervacija);
        }

        // POST: Rezervacija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rezervacija rezervacija = db.Rezervacije.Find(id);
            db.Rezervacije.Remove(rezervacija);
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

        //TODO pickaroom
        public ActionResult OdabirDatuma()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OdabirDatuma(RezervacijaView rezervacijaView)
        {
            return RedirectToAction("OdabirSobe", rezervacijaView);
        }

        public ActionResult OdabirSobe(RezervacijaView rezervacijaView)
        {
            try
            {
                var velicinaOdabira = new TimeRange(rezervacijaView.Prijava, rezervacijaView.Odjava);

                var rezerviraj = db.Rezervacije.ToList();
                var nedostupneSobeID = rezerviraj
                    .Where(r => velicinaOdabira.IntersectsWith(new TimeRange(r.Prijava, r.Odjava, true)))
                    .Select(r => r.SobaID).ToList();

                var dostupneSobe = db.Sobe.Where(r => !nedostupneSobeID.Contains(r.SobaID));

                rezervacijaView.Sobe = dostupneSobe;
                ViewBag.GostID = new SelectList((from s in db.Gosti.ToList()
                                                 select new
                                                 {
                                                     GostID= s.GostID,
                                                     Ime = s.Prezime
                                                 }),
                                               "GostID", "Ime", null);
              
                return View(rezervacijaView);

            }
            catch(SystemException exception)
            {
                logger.Error(exception);
                return View("Error", new HandleErrorInfo(exception, "Rezervacija", "OdabirSobe"));
            }
        }
    }
}
