using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoteliTest.DTO
{
    public class GostDTO
    {
        public int GostID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public string Email { get; set; }
        public string Adresa { get; set; }
    }
}