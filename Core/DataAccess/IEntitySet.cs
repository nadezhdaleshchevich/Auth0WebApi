namespace DataAccess
{
    public interface IEntitySet<TEntity> : IQueryable<TEntity>
        where TEntity : class
    {
        TEntity Add(TEntity entity);
        TEntity Remove(TEntity entity);
    }
}
