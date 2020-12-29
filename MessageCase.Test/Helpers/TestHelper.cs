using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageCase.Test.Helpers
{
    public class TestHelper
    {
        public static Mock<DbSet<T>> GetDbSetMoq<T> (IEnumerable<T> list) where T : class
        {
            var dbSetMoq = new Mock<DbSet<T>>();

            var listEntities = list.AsQueryable();

            dbSetMoq.As<IQueryable<T>>()
                .Setup(x => x.GetEnumerator())
                .Returns(listEntities.GetEnumerator());

            dbSetMoq.As<IQueryable<T>>()
                .Setup(x => x.Expression)
                .Returns(listEntities.AsQueryable().Expression);

            dbSetMoq.As<IQueryable<T>>()
                .Setup(x => x.Provider)
                .Returns(listEntities.AsQueryable().Provider);

            return dbSetMoq;
        }

        
    }
}
