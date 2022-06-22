using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Services.Tests.Data.Fakes;

namespace DataAccess.Services.Tests.Data.Extensions
{
    public static class AsyncEnumerableExtensions
    {
        public static IQueryable<TEntity> AsAsyncEnumerable<TEntity>(this IEnumerable<TEntity> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return new FakeAsyncEnumerable<TEntity>(source);
        }
    }
}
