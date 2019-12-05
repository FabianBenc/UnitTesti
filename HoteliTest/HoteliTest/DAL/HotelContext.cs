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
        public virtual DbSet<Gost> Gosti { get; set; }
        public virtual DbSet<Rezervacija> Rezervacije { get; set; }
        public virtual DbSet<Soba> Sobe { get; set; }
        public virtual DbSet<Racun> Racuni { get; set; }
        public virtual DbSet<Hotel> Hoteli { get; set; }
        public virtual DbSet<TipSobe> TipSoba { get; set; }
        public virtual DbSet<Usluga> Usluge { get; set; }
        public virtual DbSet<StavkaRacuna> StavkeRacuna { get; set; }

        public virtual void ChangeState<T>(T item) where T : class
        {
            Entry(item).State = EntityState.Modified;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

           modelBuilder.Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2"));

        }
    }
}