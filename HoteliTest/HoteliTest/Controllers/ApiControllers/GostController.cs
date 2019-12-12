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
        private IHotelAC context = new HotelContext();

        public GostController() { }

        public GostController(IHotelAC context)
        {
            this.context = context;
        }

        [HttpGet]
        public IHttpActionResult GetGuests()
        {
            return Ok(context.Gosti.Select(Mapper.Map<Gost, GostDTO>).ToList());
        }

        [HttpGet]
        public IHttpActionResult GetGuest(int id)
        {
            var guests = context.Gosti.FirstOrDefault(x => x.GostID == id);

            if (guests == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Gost, GostDTO>(guests));
        }

        [HttpPost]
        public IHttpActionResult CreateGuest([FromBody]GostDTO guestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            context.Gosti.Add(Mapper.Map<GostDTO, Gost>(guestDto));
            context.SaveChanges();
            var guestID = context.Gosti.ToList().Last().GostID;

            return Content(HttpStatusCode.Created, guestID);
        }

        [HttpDelete]
        public IHttpActionResult DeleteGuest(int id)
        {
            var guest = context.Gosti.SingleOrDefault(x => x.GostID == id);

            if (guest == null)
            {
                return BadRequest("There doesnt exist a guest with that id");
            }

            context.Gosti.Remove(guest);
            context.SaveChanges();

            return Ok("Guest removed succsessfully");
        }

        [HttpPut]
        public IHttpActionResult UpdateGuest(GostDTO guestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            if (context.Gosti.Any(x => x.GostID == guestDto.GostID))
            {
                var findGuest = context.Gosti.Find(guestDto.GostID);
                Mapper.Map(guestDto, findGuest);
                context.SaveChanges();
                return Ok("Guest succsefully updated");
            }

            return NotFound();
        }
    }
}
