using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    internal class EntitySet<TEntity> : EntitySetBase<TEntity>
        where TEntity : class
    {
        public EntitySet(DbSet<TEntity> dbSet)
        {
            DbSet = dbSet;
        }

        internal DbSet<TEntity> DbSet { get; }

        protected override IQueryable<TEntity> Queryable => DbSet;

        public override TEntity Add(TEntity entity)
        {
            var result = DbSet.Add(entity);

            return result.Entity;
        }

        public override TEntity Remove(TEntity entity)
        {
            var result = DbSet.Remove(entity);

            return result.Entity;
        }
    }
}
