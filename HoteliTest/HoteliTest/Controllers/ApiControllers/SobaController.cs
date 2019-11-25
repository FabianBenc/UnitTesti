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
    public class SobaController : ApiController
    {
        private HotelContext db = new HotelContext();

        [HttpGet]
        public IEnumerable<SobaDTO> Sobe()
        {
            return db.Sobe.Select(Mapper.Map<Soba, SobaDTO>).ToList();
        }

        [HttpGet]
        public IHttpActionResult Soba(int id)
        {
            var soba = db.Sobe.Find(id);
            if (soba == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Soba, SobaDTO>(db.Sobe.Find(id)));
        }

        [HttpPost]
        public IHttpActionResult DodajSobu(SobaDTO soba)
        {
            if (!ModelState.IsValid)
            {
                BadRequest("Krivi unos");
            }

            var hotel = db.Sobe.Count(r => r.HotelID == soba.HotelID);
            if (hotel == 0)
            {
                return BadRequest("Hotel ne postoji");
            }

            db.Sobe.Add(Mapper.Map<SobaDTO, Soba>(soba));
            db.SaveChanges();

            return Ok("Dodana je nova soba");
        }

        [HttpPut]
        public IHttpActionResult UrediSobu(SobaDTO soba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trazenaSoba = db.Sobe.Find(soba.SobaID);

            if (trazenaSoba == null)
            {
                return NotFound();
            }

            var hotel = db.Sobe.Count(r => r.HotelID == soba.HotelID);
            if (hotel == 0)
            {
                return BadRequest("Hotel ne postoji");
            }

            var tipsobe = db.Sobe.Count(r => r.TipSobeID == soba.TipSobeID);
            if (tipsobe == 0)
            {
                return BadRequest("Tip sobe ne postoji");
            }

            Mapper.Map(soba, trazenaSoba);
            db.SaveChanges();

            return Ok(soba);
        }

        [HttpDelete]
        public IHttpActionResult Obrisi(int id)
        {
            var soba = db.Sobe.Find(id);
            if (soba == null)
            {
                return NotFound();
            }

            db.Sobe.Remove(soba);
            db.SaveChanges();

            return Ok(Mapper.Map<Soba, SobaDTO>(soba));
        }
    }
}
