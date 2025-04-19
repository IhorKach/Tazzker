namespace Tazzker.Client.Interfaces
{
    public interface ILocalDb<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task AddOrUpdateAsync(T item);
        Task SoftDeleteAsync(string id);
    }
}
