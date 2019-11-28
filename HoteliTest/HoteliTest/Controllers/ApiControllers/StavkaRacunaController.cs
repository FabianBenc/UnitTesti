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
    public class StavkaRacunaController : ApiController
    {
        private HotelContext db = new HotelContext();

        [HttpGet]
        public IEnumerable<StavkaRacunaDTO> StavkeRacuna()
        {
            return db.StavkeRacuna.Select(Mapper.Map<StavkaRacuna, StavkaRacunaDTO>).ToList();
        }

        [HttpGet]
        public IHttpActionResult StavkaRacuna(int id)
        {
            var stavkaRacuna = db.StavkeRacuna.Find(id);
            if (stavkaRacuna == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<StavkaRacuna, StavkaRacunaDTO>(db.StavkeRacuna.Find(id)));
        }

        [HttpPost]
        public IHttpActionResult DodajStavku(StavkaRacunaDTO stavka)
        {
            if (!ModelState.IsValid)
            {
                BadRequest("Krivi unos");
            }

            var usluga = db.StavkeRacuna.Count(r => r.UslugaID == stavka.UslugaID);
            if (usluga == 0)
            {
                return BadRequest("Usluga ne postoji");
            }

            var racun = db.StavkeRacuna.Count(r => r.RacunID == stavka.RacunID);
            if (racun == 0)
            {
                return BadRequest("Ne postoji racun");
            }

            db.StavkeRacuna.Add(Mapper.Map<StavkaRacunaDTO, StavkaRacuna>(stavka));
            db.SaveChanges();

            return Ok(stavka);
        }

        [HttpPut]
        public IHttpActionResult UrediStavku(StavkaRacunaDTO stavka)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trazenastavka = db.StavkeRacuna.Find(stavka.StavkaRacunaID);

            if (trazenastavka == null)
            {
                return NotFound();
            }

            var usluga = db.StavkeRacuna.Count(r => r.UslugaID == stavka.UslugaID);
            if (usluga == 0)
            {
                return BadRequest("Racun ne postoji");
            }

            Mapper.Map(stavka, trazenastavka);
            db.SaveChanges();

            return Ok(stavka);
        }

        [HttpDelete]
        public IHttpActionResult Obrisi(int id)
        {
            var stavka = db.StavkeRacuna.Find(id);
            if (stavka == null)
            {
                return NotFound();
            }

            db.StavkeRacuna.Remove(stavka);
            db.SaveChanges();

            return Ok(Mapper.Map<StavkaRacuna, StavkaRacunaDTO>(stavka));
        }

    }
}
