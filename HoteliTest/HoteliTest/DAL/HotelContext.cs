using HoteliTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace HoteliTest.DAL
{
    public class HotelContext: DbContext, IHotelAC
    {
        public DbSet<Gost> Gosti { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<Soba> Sobe { get; set; }
        public DbSet<Racun> Racuni { get; set; }
        public DbSet<Hotel> Hoteli { get; set; }
        public DbSet<TipSobe> TipSoba { get; set; }
        public DbSet<Usluga> Usluge { get; set; }
        public DbSet<StavkaRacuna> StavkeRacuna { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2"));

        }
    }
}