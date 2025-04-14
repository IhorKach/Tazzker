using Tazzker.Domain;

namespace Tazzker.Application.Interfaces
{
    public interface IListRepository
    {
        Task<IEnumerable<List>> GetAllListsAsync(Guid userId);
        Task<List?> GetListByIdAsync(Guid listId, Guid userId);
        System.Threading.Tasks.Task CreateListAsync(List newList);
        System.Threading.Tasks.Task UpdateListAsync(List updateList);
        System.Threading.Tasks.Task DeleteListAsync(List listToDelete);
    }
}
