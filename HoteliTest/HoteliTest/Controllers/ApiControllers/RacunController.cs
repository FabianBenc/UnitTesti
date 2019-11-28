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
    public class RacunController : ApiController
    {
        private HotelContext db = new HotelContext();
        [HttpGet]
        public IEnumerable<RacunDTO> Racuni()
        {
            return db.Racuni.Select(Mapper.Map<Racun, RacunDTO>).ToList();
        }

        [HttpGet]
        public IHttpActionResult Racun(int id)
        {
            var racun = db.Racuni.Find(id);
            if (racun == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Racun, RacunDTO>(db.Racuni.Find(id)));
        }

        [HttpPost]
        public IHttpActionResult DodajRacun(RacunDTO racun)
        {
            if (!ModelState.IsValid)
            {
                BadRequest("Krivi unos");
            }

            db.Racuni.Add(Mapper.Map<RacunDTO, Racun>(racun));
            db.SaveChanges();

            return Ok(racun);
        }

        [HttpPut]
        public IHttpActionResult UrediRacun(RacunDTO racun)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rezervacija = db.Racuni.Count(r => r.RezervacijaID == racun.RezervacijaID);
            if (rezervacija == 0)
            {
                return BadRequest("Rezervacija ne postoji");
            }

            var trazeniracun = db.Racuni.Find(racun.RacunID);

            if (trazeniracun == null)
            {
                return NotFound();
            }

            Mapper.Map(racun, trazeniracun);
            db.SaveChanges();

            return Ok(racun);
        }

        [HttpDelete]
        public IHttpActionResult Obrisi(int id)
        {
            var racun = db.Racuni.Find(id);
            if (racun == null)
            {
                return NotFound();
            }

            db.Racuni.Remove(racun);
            db.SaveChanges();

            return Ok(Mapper.Map<Racun, RacunDTO>(racun));
        }
    }
}
