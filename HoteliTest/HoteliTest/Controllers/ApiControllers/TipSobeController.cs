using AutoMapper;
using HoteliTest.DAL;
using HoteliTest.DTO;
using HoteliTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HoteliTest.Controllers.ApiControllers
{
    public class TipSobeController : ApiController
    {
        private HotelContext db = new HotelContext();
        [HttpGet]
        public IEnumerable<TipSobeDTO> TipSoba()
        {
            return db.TipSoba.Select(Mapper.Map<TipSobe, TipSobeDTO>).ToList();
        }

        [HttpGet]
        public IHttpActionResult TipSobe(int id)
        {
            var tipSobe = db.TipSoba.Find(id);
            if (tipSobe == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<TipSobe, TipSobeDTO>(db.TipSoba.Find(id)));
        }

        [HttpPost]
        public IHttpActionResult DodajTip(TipSobeDTO tipsobe)
        {
            if (!ModelState.IsValid)
            {
                BadRequest("Krivi unos");
            }

            db.TipSoba.Add(Mapper.Map<TipSobeDTO, TipSobe>(tipsobe));
            db.SaveChanges();

            return Ok("Dodan je novi tip sobe");
        }

        [HttpPut]
        public IHttpActionResult UrediTip(TipSobeDTO tipSobe)
        {
            if (!ModelState.IsValid)
            {
                BadRequest("Krivi unos");
            }

            var trazenitip = db.TipSoba.Find(tipSobe.TipSobeID);

            if (trazenitip == null)
            {
                return NotFound();
            }

            Mapper.Map(tipSobe, trazenitip);
            db.SaveChanges();

            return Ok(tipSobe);
        }

        [HttpDelete]
        public IHttpActionResult ObrisiTip(int id)
        {
            var tipsobe = db.TipSoba.Find(id);
            if (tipsobe == null)
            {
                return NotFound();
            }

            db.TipSoba.Remove(tipsobe);
            db.SaveChanges();

            return Ok(Mapper.Map<TipSobe, TipSobeDTO>(tipsobe));
        }

    }
}
