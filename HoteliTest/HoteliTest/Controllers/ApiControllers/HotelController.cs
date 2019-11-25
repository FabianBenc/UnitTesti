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
    public class HotelController : ApiController
    {
        private HotelContext db = new HotelContext();

        [HttpGet]
        public IEnumerable<HotelDTO> Hoteli()
        {
            return db.Hoteli.Select(Mapper.Map<Hotel, HotelDTO>).ToList();
        }

        [HttpGet]
        public IHttpActionResult Hotel(int id)
        {
            var hotel = db.Hoteli.Find(id);
            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Hotel, HotelDTO>(db.Hoteli.Find(id)));
        }

        [HttpPost]
        public IHttpActionResult DodajHotel(HotelDTO hotel)
        {
            if (!ModelState.IsValid)
            {
                BadRequest("Krivi unos");
            }

            db.Hoteli.Add(Mapper.Map<HotelDTO, Hotel>(hotel));
            db.SaveChanges();

            return Ok("Dodan je novi hotel");
        }

        [HttpPut]
        public IHttpActionResult UrediHotel(HotelDTO hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nadeniHotel = db.Hoteli.Find(hotel.HotelID);

            if (nadeniHotel == null)
            {
                return NotFound();
            }

            Mapper.Map(hotel, nadeniHotel);
            db.SaveChanges();

            return Ok(hotel);
        }

        [HttpDelete]
        public IHttpActionResult ObrisiHotel(int id)
        {
            var hotel = db.Hoteli.Find(id);
            if (hotel == null)
            {
                return NotFound();
            }

            db.Hoteli.Remove(hotel);
            db.SaveChanges();

            return Ok(Mapper.Map<Hotel, HotelDTO>(hotel));
        }
    }
}
