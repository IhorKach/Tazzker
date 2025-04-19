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
            return all.Where(x => !x.IsDeleted).OrderByDescending(x => x.UpdatedAt).ToList();
        }

        public async Task<NoteModel?> GetByIdAsync(string id)
        {
            var all = await GetAllAsync();
            return all.FirstOrDefault(x => x.Id == id);
        }

        public async Task AddOrUpdateAsync(NoteModel item)
        {
            item.UpdatedAt = DateTime.UtcNow;
            await _db.AddAsync(StoreName, item);
        }

        public async Task SoftDeleteAsync(string id)
        {
            var note = await GetByIdAsync(id);
            if (note is null) return;

            note.IsDeleted = true;
            note.IsSynced = false;
            note.UpdatedAt = DateTime.UtcNow;

            await AddOrUpdateAsync(note);
        }
    }
}
