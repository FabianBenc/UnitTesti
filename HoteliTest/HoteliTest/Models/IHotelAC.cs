using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteliTest.Models
{
    public interface IHotelAC: IDisposable
    {
        DbSet<Gost> Gosti { get; }
        DbSet<Rezervacija> Rezervacije { get; }
        DbSet<Soba> Sobe { get; }
        DbSet<Racun> Racuni { get; }
        DbSet<Hotel> Hoteli { get; }
        DbSet<TipSobe> TipSoba { get; }
        DbSet<Usluga> Usluge { get; }
        DbSet<StavkaRacuna> StavkeRacuna { get; }

        int SaveChanges();
    }
}
