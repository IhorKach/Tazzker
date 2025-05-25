using Tazzker.Client.Data.Models;
using Tazzker.Client.Interfaces;
using Tazzker.Client.Services;
namespace Tazzker.Client.Data.LocalDb
{
    public class NoteLocalDb : ILocalDb<NoteModel>
    {
        private readonly IndexedDbBridge _db;
        private const string StoreName = "Notes";

        public NoteLocalDb(IndexedDbBridge db)
        {
            _db = db;
        }

        public async Task<List<NoteModel>> GetAllAsync()
        {
            var all = await _db.GetAllAsync<NoteModel>(StoreName);
            return all.Where(x => !x.PermDeleted/*IsDeleted*/).OrderByDescending(x => x.UpdatedAt).ToList();
        }

        public async Task<NoteModel?> GetByIdAsync(Guid id)
        {
            var all = await GetAllAsync();
            return all.FirstOrDefault(x => x.Id == id);
        }

        public async Task AddOrUpdateAsync(NoteModel item)
        {
            item.UpdatedAt = DateTime.UtcNow;
            item.IsSynced = false;
            await _db.AddAsync(StoreName, item);
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var note = await GetByIdAsync(id);
            if (note is null) return;

            //note.IsDeleted = true;
            note.PermDeleted = true;
            await AddOrUpdateAsync(note);
        }

        public async Task<List<NoteModel>> GetAllRawAsync()
        {
            return await _db.GetAllAsync<NoteModel>(StoreName);
        }
    }
}
