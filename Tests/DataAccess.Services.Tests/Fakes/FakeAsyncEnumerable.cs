using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace DataAccess.Services.Tests.Fakes
{
    public class FakeAsyncEnumerable<TEntity> : EnumerableQuery<TEntity>, IAsyncEnumerable<TEntity>, IQueryable<TEntity>
    {
        public FakeAsyncEnumerable(IEnumerable<TEntity> enumerable) : base(enumerable)
        { }

        public FakeAsyncEnumerable(Expression expression) : base(expression)
        { }

        public IAsyncEnumerator<TEntity> GetEnumerator()
        {
            return new FakeIAsyncEnumerator<TEntity>(this.AsEnumerable().GetEnumerator());
        }

        public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            return new FakeIAsyncEnumerator<TEntity>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider => new FakeAsyncQueryProvider<TEntity>(this);
    }
}
