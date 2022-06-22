using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Services.Tests.Fakes
{
    public class FakeIAsyncEnumerator<TEntity> : IAsyncEnumerator<TEntity>
    {
        private readonly IEnumerator<TEntity> _inner;

        public FakeIAsyncEnumerator(IEnumerator<TEntity> inner)
        {
            _inner = inner;
        }

        public async ValueTask DisposeAsync()
        {
            _inner.Dispose();
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            return _inner.MoveNext();
        }

        public TEntity Current => _inner.Current;
    }
}
