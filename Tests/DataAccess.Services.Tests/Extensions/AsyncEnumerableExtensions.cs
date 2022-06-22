using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Services.Tests.Fakes;

namespace DataAccess.Services.Tests.Extensions
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
