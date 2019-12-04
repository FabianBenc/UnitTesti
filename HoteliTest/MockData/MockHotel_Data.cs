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
        public static void setupSet<T>(ICollection<T> data, Mock<DbSet<T>> set) where T : class
        {
            var dataQ = data.AsQueryable<T>();
            set.As<IQueryable<T>>().Setup(m => m.Provider).Returns(() => dataQ.Provider);
        }
    }
}
