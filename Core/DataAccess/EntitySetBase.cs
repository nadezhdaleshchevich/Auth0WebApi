using System.Collections;
using System.Linq.Expressions;

namespace DataAccess
{
    public abstract class EntitySetBase<TEntity> : IQueryable<TEntity>, IEntitySet<TEntity>
        where TEntity : class
    {
        public Expression Expression => Queryable.Expression;

        public Type ElementType => Queryable.ElementType;

        public IQueryProvider Provider => Queryable.Provider;

        protected abstract IQueryable<TEntity> Queryable { get; }

        public IEnumerator<TEntity> GetEnumerator() => Queryable.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => (Queryable as IEnumerable).GetEnumerator();

        public abstract TEntity Add(TEntity entity);

        public abstract TEntity Remove(TEntity entity);
    }
}
