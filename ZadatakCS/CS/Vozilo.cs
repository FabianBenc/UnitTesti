using System;
using System.Collections.Generic;
using System.Text;

namespace ZadatakCS
{
    public abstract class Vozilo
    {
        public string NazivVozila { get; set; }
        public override string ToString()
        {
            return $"{nameof(NazivVozila)}:{NazivVozila}";
        }
    }
}
