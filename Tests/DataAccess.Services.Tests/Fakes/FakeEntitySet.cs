using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Services.Tests.Extensions;


namespace DataAccess.Services.Tests.Fakes
{
    internal class FakeEntitySet<TEntity> : EntitySetBase<TEntity>, IAsyncEnumerable<TEntity>
        where TEntity : class
    {
        private readonly IList<TEntity> _list;
        private readonly IQueryable<TEntity> _queryable;

        public FakeEntitySet(IList<TEntity> list)
        {
            _list = list;
            _queryable = list.AsAsyncEnumerable();
        }

        protected override IQueryable<TEntity> Queryable => _queryable;

        public override TEntity Add(TEntity entity)
        {
            _list.Add(entity);
            return entity;
        }

        public override TEntity Remove(TEntity entity)
        {
            _list.Remove(entity);
            return entity;
        }

        public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            return new FakeEntitySetAsyncEnumerator<TEntity>(GetEnumerator());
        }

        private class FakeEntitySetAsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _enumerator;

            public FakeEntitySetAsyncEnumerator(IEnumerator<T> enumerator)
            {
                _enumerator = enumerator;
            }

            public async ValueTask DisposeAsync()
            {
                _enumerator.Dispose();
            }

            public async ValueTask<bool> MoveNextAsync()
            {
                return _enumerator.MoveNext();
            }

            public T Current => _enumerator.Current;
        }
    }
}
