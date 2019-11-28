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
    public class GostController : ApiController
    {
        private HotelContext db = new HotelContext();

        [HttpGet]
        public IEnumerable<GostDTO> Gosti()
        {
            return db.Gosti.Select(Mapper.Map<Gost, GostDTO>).ToList();
        }

        [HttpGet]
        public IHttpActionResult Gost(int id)
        {
            var gost = db.Gosti.Find(id);
            if (gost == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Gost, GostDTO>(db.Gosti.Find(id)));
        }

        [HttpPost]
        public IHttpActionResult DodajGosta(GostDTO gost)
        {
            if (!ModelState.IsValid)
            {
                BadRequest("Krivi unos");
            }

            db.Gosti.Add(Mapper.Map<GostDTO, Gost>(gost));
            db.SaveChanges();

            return Ok(gost);
        }

        [HttpPut]
        public IHttpActionResult UrediGosta(GostDTO gost)
        {

            if (!ModelState.IsValid)
            {
                BadRequest("Krivi unos");
            }

            var nadeniGost = db.Gosti.Find(gost.GostID);

            if (nadeniGost == null)
            {
                return NotFound();
            }

            Mapper.Map(gost, nadeniGost);
            db.SaveChanges();

            return Ok(gost);
        }

        [HttpDelete]
        public IHttpActionResult ObrisiGosta(int id)
        {
            var gost = db.Gosti.Find(id);
            if (gost == null)
            {
                return NotFound();
            }
            db.Gosti.Remove(gost);
            db.SaveChanges();

            return Ok(Mapper.Map<Gost, GostDTO>(gost));
        }
    }
}
