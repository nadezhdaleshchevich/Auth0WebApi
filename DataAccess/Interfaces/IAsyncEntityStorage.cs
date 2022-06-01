namespace DataAccess.Interfaces
{
    public interface IAsyncEntityStorage
    {
        Task<int> SaveChangesAsync();
    }
}
