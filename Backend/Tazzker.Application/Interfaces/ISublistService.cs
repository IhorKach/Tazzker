using Tazzker.Application.DTOs;

namespace Tazzker.Application.Interfaces
{
    public interface ISublistService
    {
        Task<IEnumerable<SublistDTO>> GetSublistsAsync();
        Task<bool> SyncSublistsAsync(IEnumerable<SublistDTO> sublists);
        Task<bool> DeleteSublistsAsync(IEnumerable<Guid> sublistIds);
    }
}
