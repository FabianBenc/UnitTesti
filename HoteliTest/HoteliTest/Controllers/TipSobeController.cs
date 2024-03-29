﻿using System;
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
    public class TipSobeController : BaseController
    {
        

        public TipSobeController() { }

        public TipSobeController(IHotelAC context)
        {
            db = context;
        }

        // GET: TipSobe
        public ActionResult Index()
        {
            return View(db.TipSoba.ToList());
        }

        // GET: TipSobe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipSobe tipSobe = db.TipSoba.Find(id);
            if (tipSobe == null)
            {
                return HttpNotFound();
            }
            return View(tipSobe);
        }

        // GET: TipSobe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipSobe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipSobeID,OpisSobe,CijenaPoNoci")] TipSobe tipSobe)
        {
            if (ModelState.IsValid)
            {
                db.TipSoba.Add(tipSobe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipSobe);
        }

        // GET: TipSobe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipSobe tipSobe = db.TipSoba.Find(id);
            if (tipSobe == null)
            {
                return HttpNotFound();
            }
            return View(tipSobe);
        }

        // POST: TipSobe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipSobeID,OpisSobe,CijenaPoNoci")] TipSobe tipSobe)
        {
            if (ModelState.IsValid)
            {
               // db.ChangeState<TipSobe>(tipSobe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipSobe);
        }

        // GET: TipSobe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipSobe tipSobe = db.TipSoba.Find(id);
            if (tipSobe == null)
            {
                return HttpNotFound();
            }
            return View(tipSobe);
        }

        // POST: TipSobe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipSobe tipSobe = db.TipSoba.Find(id);
            db.TipSoba.Remove(tipSobe);
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
