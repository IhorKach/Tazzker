using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Application.DTOs;

namespace Tazzker.Application.Interfaces
{
    public interface IListService
    {
        Task<IEnumerable<ListDTO>> GetListsAsync();
        Task<bool> SyncListsAsync(IEnumerable<ListDTO> lists);
        Task<bool> DeleteListsAsync(IEnumerable<Guid> listIds);
    }
}
