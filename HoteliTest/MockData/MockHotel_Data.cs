using HoteliTest.DAL;
using HoteliTest.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockData
{
    public static class MockHotel_Data
    {
        public static void SetupSet<T>(ICollection<T> data, Mock<DbSet<T>> set) where T : class
        {
            var dataQ = data.AsQueryable<T>();
            set.As<IQueryable<T>>().Setup(m => m.Provider).Returns(() => dataQ.Provider);
            set.As<IQueryable<T>>().Setup(m => m.Expression).Returns(() => dataQ.Expression);
            set.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(() => dataQ.ElementType);
            set.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => dataQ.GetEnumerator());

            set.Setup(i => i.Include(It.IsAny<string>())).Returns(set.Object);
            set.Setup(a => a.Add(It.IsAny<T>())).Callback<T>((item) => data.Add(item));
            set.Setup(r => r.Remove(It.IsAny<T>())).Callback((T item) => data.Remove(item));
            set.Setup(f => f.Find(It.IsAny<object[]>())).Returns<object[]>(ids => data.FirstOrDefault(entity => { string property = entity.GetType().Name + "ID";
                return (int)typeof(T).GetProperty(property).GetValue(entity) == (int)ids[0];
            }));
        }

        public static Mock<HotelContext> Create()
        {
            var context = new Mock<HotelContext>();

            Mock<DbSet<Gost>> gostSet = new Mock<DbSet<Gost>>();
            SetUpEntity(ModelLoader.GetGosti(), context, gostSet);
            context.Setup(c => c.Gosti).Returns(gostSet.Object);

            Mock<DbSet<Hotel>> hotelSet = new Mock<DbSet<Hotel>>();
            SetUpEntity(ModelLoader.GetHoteli(), context, hotelSet);
            context.Setup(c => c.Hoteli).Returns(hotelSet.Object);

            Mock<DbSet<Soba>> sobaSet = new Mock<DbSet<Soba>>();
            SetUpEntity(ModelLoader.GetSobe(), context, sobaSet);
            context.Setup(c => c.Sobe).Returns(sobaSet.Object);

            Mock<DbSet<StavkaRacuna>> stavkaSet = new Mock<DbSet<StavkaRacuna>>();
            SetUpEntity(ModelLoader.GetStavke(), context, stavkaSet);
            context.Setup(c => c.StavkeRacuna).Returns(stavkaSet.Object);

            Mock<DbSet<Usluga>> uslugaSet = new Mock<DbSet<Usluga>>();
            SetUpEntity(ModelLoader.GetUsluge(), context, uslugaSet);
            context.Setup(c => c.Usluge).Returns(uslugaSet.Object);

            Mock<DbSet<Racun>> racunSet = new Mock<DbSet<Racun>>();
            SetUpEntity(ModelLoader.GetRacuni(), context, racunSet);
            context.Setup(c => c.Racuni).Returns(racunSet.Object);

            Mock<DbSet<TipSobe>> tipSobeSet = new Mock<DbSet<TipSobe>>();
            SetUpEntity(ModelLoader.GetTipSoba(), context, tipSobeSet);
            context.Setup(c => c.TipSoba).Returns(tipSobeSet.Object);

            Mock<DbSet<Rezervacija>> rezervacijaSet = new Mock<DbSet<Rezervacija>>();
            SetUpEntity(ModelLoader.GetRezervacije(), context, rezervacijaSet);
            context.Setup(c => c.Rezervacije).Returns(rezervacijaSet.Object);

            return context;
        }

        private static void SetUpEntity<T>(ICollection<T> data, Mock<HotelContext> context, Mock<DbSet<T>> set) where T : class
        {
            SetupSet(data, set);
            context.Setup(c => c.ChangeState((T)Activator.CreateInstance(typeof(T))));
        }

        public static void Dispose(Mock<HotelContext> context)
        {
            context.Object.Dispose();
        }
    }
}
