using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;
using Tazzker.Domain;

namespace Tazzker.Application.Services
{
    public class SublistService : ISublistService
    {
        private readonly ISublistRepository _sublistRepository;
        private readonly IUserContext _userContext;
        public SublistService(ISublistRepository sublistRepository, IUserContext userContext)
        {
            _sublistRepository = sublistRepository;
            _userContext = userContext;
        }

        public SublistDTO CreateSublistDTO(Sublist sublist)
        {
            return new SublistDTO
            {
                ListId = sublist.ListId,
                SublistId = sublist.SublistId,
                Title = sublist.Title,
                Order = sublist.Order,
                UpdatedAt = sublist.UpdatedAt,
            };
        }


        public Sublist CreateSublist(SublistDTO sublistDTO, Guid? foundSublistId = null)
        {
            if (foundSublistId.HasValue)
                sublistDTO.SublistId = foundSublistId.Value;

            return new Sublist
            {
                UserId = _userContext.UserId,
                SublistId = sublistDTO.SublistId,
                ListId = sublistDTO.ListId,
                Order = sublistDTO.Order,
                Title = sublistDTO.Title,
                UpdatedAt = sublistDTO.UpdatedAt,
            };
        }


        public async Task<IEnumerable<SublistDTO>> GetSublistsAsync()
        {
            var sublists = await _sublistRepository.GetAllSublistsAsync(_userContext.UserId);
            return sublists.Select(s => CreateSublistDTO(s)).ToList();
        }


        public async Task<bool> SyncSublistsAsync(IEnumerable<SublistDTO> sublists)
        {
            try
            {
                foreach (var sublist in sublists)
                {
                    var s = await _sublistRepository.GetSublistByIdAsync(sublist.SublistId, _userContext.UserId);

                    if (s != null)
                        await _sublistRepository.UpdateSublistAsync(CreateSublist(sublist, s.SublistId));
                    else
                        await _sublistRepository.CreateSublistAsync(CreateSublist(sublist));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> DeleteSublistsAsync(IEnumerable<Guid> sublistIds)
        {
            try
            {
                foreach (var sublistId in sublistIds)
                {
                    var s = await _sublistRepository.GetSublistByIdAsync(sublistId, _userContext.UserId);
                    if (s != null)
                        await _sublistRepository.DeleteSublistAsync(s);
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
