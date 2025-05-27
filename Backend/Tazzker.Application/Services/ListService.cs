using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;
using Tazzker.Domain;

namespace Tazzker.Application.Services
{
    public class ListService : IListService
    {
        private readonly IListRepository _listRepository;
        private readonly IUserContext _userContext;
        public ListService(IListRepository listRepository, IUserContext userContext)
        {
            _listRepository = listRepository;
            _userContext = userContext;
        }

        public ListDTO CreateListDTO(List list)
        {
            return new ListDTO
            {
                ListId = list.ListId,
                Title = list.Title,
                UpdatedAt = list.UpdatedAt,
                CreatedAt = list.CreatedAt,
                AssignedDay = list.AssignedDay
            };
        }

        public List CreateList(ListDTO listDTO, Guid? foundListId = null)
        {
            if (foundListId.HasValue)
                listDTO.ListId = foundListId.Value;

            return new List
            {
                UserId = _userContext.UserId,
                Title = listDTO.Title,
                ListId = listDTO.ListId,
                CreatedAt = listDTO.CreatedAt,
                AssignedDay = listDTO.AssignedDay,
                UpdatedAt = listDTO.UpdatedAt
            };
        }

        public async Task<IEnumerable<ListDTO>> GetListsAsync()
        {
            var lists = await _listRepository.GetAllListsAsync(_userContext.UserId);
            return lists.Select(l => CreateListDTO(l)).ToList();
        }

        public async Task<bool> SyncListsAsync(IEnumerable<ListDTO> lists)
        {
            try
            {
                foreach (var list in lists)
                {
                    Console.WriteLine($"ListId: {list.ListId}, UserId: {_userContext.UserId}");
                    var l = await _listRepository.GetListByIdAsync(list.ListId, _userContext.UserId);

                    if (l != null)
                        await _listRepository.UpdateListAsync(CreateList(list, l.ListId));
                    else
                        await _listRepository.CreateListAsync(CreateList(list));
                }
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message );
                return false;
            }
        }

        public async Task<bool> DeleteListsAsync(IEnumerable<Guid> listIds)
        {
            try
            {
                foreach (var listId in listIds)
                {
                    var l = await _listRepository.GetListByIdAsync(listId, _userContext.UserId);
                    if (l != null)
                        await _listRepository.DeleteListAsync(l);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
