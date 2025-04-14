using Tazzker.Domain;

namespace Tazzker.Application.Interfaces
{
    public interface ISublistRepository
    {
        Task<IEnumerable<Sublist>> GetAllSublistsAsync(Guid userId);
        Task<Sublist?> GetSublistByIdAsync(Guid sublistId, Guid userId);
        System.Threading.Tasks.Task CreateSublistAsync(Sublist newSublist);
        System.Threading.Tasks.Task UpdateSublistAsync(Sublist updateSublist);
        System.Threading.Tasks.Task DeleteSublistAsync(Sublist sublistToDelete);
    }
}
