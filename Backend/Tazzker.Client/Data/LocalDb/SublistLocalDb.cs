using Tazzker.Client.Data.Models;
using Tazzker.Client.Interfaces;
using Tazzker.Client.Services;

namespace Tazzker.Client.Data.LocalDb
{
	public class SublistLocalDb : ILocalDb<SublistModel>
	{
		private readonly IndexedDbBridge _db;
		private const string StoreName = "Sublists";

		public SublistLocalDb(IndexedDbBridge db)
		{
			_db = db;
		}

		public async Task<List<SublistModel>> GetAllAsync()
		{
			var all = await _db.GetAllAsync<SublistModel>(StoreName);
			return all.Where(x => !x.IsDeleted).OrderBy(x => x.Order).ToList();
		}

		public async Task<SublistModel?> GetByIdAsync(string id)
		{
			var all = await GetAllAsync();
			return all.FirstOrDefault(x => x.Id == id);
		}

		public async Task AddOrUpdateAsync(SublistModel item)
		{
			item.UpdatedAt = DateTime.UtcNow;
			await _db.AddAsync(StoreName, item);
		}

		public async Task SoftDeleteAsync(string id)
		{
			var sublist = await GetByIdAsync(id);
			if (sublist is null) return;

			sublist.IsDeleted = true;
			sublist.IsSynced = false;
			sublist.UpdatedAt = DateTime.UtcNow;

			await AddOrUpdateAsync(sublist);
		}
	}
}
