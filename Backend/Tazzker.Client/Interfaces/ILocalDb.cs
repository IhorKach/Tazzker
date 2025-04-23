namespace Tazzker.Client.Interfaces
{
    public interface ILocalDb<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task AddOrUpdateAsync(T item);
        Task SoftDeleteAsync(Guid id);
    }
}
