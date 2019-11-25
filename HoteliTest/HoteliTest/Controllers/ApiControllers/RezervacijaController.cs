using AutoMapper;
using HoteliTest.DAL;
using HoteliTest.DTO;
using HoteliTest.Models;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HoteliTest.Controllers.ApiControllers
{
    public class RezervacijaController : ApiController
    {
        private HotelContext db = new HotelContext();

        [HttpGet]
        public IEnumerable<RezervacijaDTO> Rezervacije()
        {
            return db.Rezervacije.Select(Mapper.Map<Rezervacija, RezervacijaDTO>).ToList();
        }

        [HttpGet]
        public IHttpActionResult Rezervacija(int id)
        {
            var rezervacija = db.Rezervacije.Find(id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Rezervacija, RezervacijaDTO>(db.Rezervacije.Find(id)));
        }

        [HttpPost]
        public IHttpActionResult Dodaj([FromBody]RezervacijaDTO rezervacija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Krivi unos");
            }

            var gost = db.Gosti.FirstOrDefault(x => x.GostID == rezervacija.GostID);

            if (gost == null)
            {
                return BadRequest("Gost ne postoji");
            }

            var soba = db.Sobe.FirstOrDefault(x => x.SobaID == rezervacija.SobaID);
            if (soba == null)
            {
                return BadRequest("Soba ne postoji");
            }

            var rangeFromSelect = new TimeRange(rezervacija.Prijava, rezervacija.Odjava);
            var rezervacija1 = db.Rezervacije.ToList();
            var unavailableRoomsId = rezervacija1
                .Where(r => rangeFromSelect.IntersectsWith(new TimeRange(r.Prijava, r.Odjava, true)))
                .Select(r => r.SobaID).ToList();

            if (unavailableRoomsId.Contains(rezervacija.SobaID))
            {
                return BadRequest();
            }

            db.Rezervacije.Add(Mapper.Map<RezervacijaDTO, Rezervacija>(rezervacija));
            db.SaveChanges();
            return Content(HttpStatusCode.Created, rezervacija);
        }

        [HttpPut]
        public IHttpActionResult Uredi(RezervacijaDTO rezervacija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trezervacija = db.Rezervacije.Find(rezervacija.RezervacijaID);
            if (trezervacija == null)
            {
                return NotFound();
            }

            var gost = db.Rezervacije.Count(r => r.GostID == rezervacija.GostID);
            if (gost == 0)
            {
                return BadRequest("Gost ne postoji");
            }

            var soba = db.Rezervacije.Count(r => r.SobaID == rezervacija.SobaID);
            if (soba == 0)
            {
                return BadRequest("Soba ne postoji");
            }

            var slobodnaSoba = SlobodnaSoba(rezervacija);

            if (!slobodnaSoba)
                return BadRequest("Soba je zauzeta");

            Mapper.Map(rezervacija, trezervacija);
            db.SaveChanges();

            return Ok(rezervacija);
        }

        [HttpDelete]
        public IHttpActionResult Obrisi(int id)
        {
            var rezervacija = db.Rezervacije.Find(id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            db.Rezervacije.Remove(rezervacija);
            db.SaveChanges();

            return Ok(Mapper.Map<Rezervacija, RezervacijaDTO>(rezervacija));
        }

        private bool SlobodnaSoba(RezervacijaDTO rezervacija)
        {
            TimeRange nRange = new TimeRange(rezervacija.Prijava, rezervacija.Odjava);

            var rez = db.Rezervacije.Where(r => r.SobaID == rezervacija.SobaID);
            if (rez.Any())
            {
                foreach (var r in rez)
                {
                    TimeRange range = new TimeRange(r.Prijava, r.Odjava);
                    if (nRange.IntersectsWith(range))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
