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
    public class UslugaController : ApiController
    {
        private HotelContext db = new HotelContext();
        [HttpGet]
        public IEnumerable<UslugaDTO> Usluge()
        {
            return db.Usluge.Select(Mapper.Map<Usluga, UslugaDTO>).ToList();
        }

        [HttpGet]
        public IHttpActionResult Usluga(int id)
        {
            var usluga = db.Usluge.Find(id);
            if (usluga == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Usluga, UslugaDTO>(db.Usluge.Find(id)));
        }

        [HttpPost]
        public IHttpActionResult DodajUslugu(UslugaDTO usluga)
        {
            if (!ModelState.IsValid)
            {
                BadRequest("Krivi unos");
            }

            db.Usluge.Add(Mapper.Map<UslugaDTO, Usluga>(usluga));
            db.SaveChanges();

            return Ok("Dodana nova usluga");
        }

        [HttpPut]
        public IHttpActionResult UrediUslugu(UslugaDTO usluga)
        {
            if (!ModelState.IsValid)
            {
                BadRequest("Krivi unos");
            }
            var nadenaUsluga = db.Usluge.Find(usluga.UslugaID);

            if (nadenaUsluga == null)
            {
                return NotFound();
            }

            Mapper.Map(usluga, nadenaUsluga);
            db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult ObrisiUslugu(int id)
        {
            var usluga = db.Usluge.Find(id);
            if (usluga == null)
            {
                return NotFound();
            }

            db.Usluge.Remove(usluga);
            db.SaveChanges();

            return Ok(Mapper.Map<Usluga, UslugaDTO>(usluga));
        }

    }
}
