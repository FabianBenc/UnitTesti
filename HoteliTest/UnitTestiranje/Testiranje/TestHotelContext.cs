using HoteliTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestiranje.Testiranje
{
    public class TestHotelContext : IHotelAC
    {
        public TestHotelContext()
        {
            this.Hoteli = new TestHotelDbSet();
            this.Gosti = new TestGostDbSet();
            this.TipSoba = new TestTipSobeDbSet();
            this.Sobe = new TestSobaDbSet();
            this.Rezervacije = new TestRezervacijaDbSet();
            this.StavkeRacuna = new TestStavkaRacunaDbSet();
            this.Racuni = new TestRacunDbSet();
            this.Usluge = new TestUslugaDbSet();
        }

        public DbSet<Gost> Gosti { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<Soba> Sobe { get; set; }
        public DbSet<Racun> Racuni { get; set; }
        public DbSet<Hotel> Hoteli { get; set; }
        public DbSet<TipSobe> TipSoba { get; set; }
        public DbSet<Usluga> Usluge { get; set; }
        public DbSet<StavkaRacuna> StavkeRacuna { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified() { }
        public void Dispose() { }

        public DbEntityEntry Entry(object entity)
        {
            return null;
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return null;
        }
    }
}
