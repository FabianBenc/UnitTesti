using System;
using System.Collections.Generic;
using System.Text;

namespace ZadatakCS
{
    public class Tip
    {
        public Type TipVozila { get; private set; }
        public Tip(Type tipVozila)
        {
            TipVozila = tipVozila;
        }
    }
}
