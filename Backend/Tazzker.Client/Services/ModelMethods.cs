using Tazzker.Client.Data.Models;
using Tazzker.Client.Interfaces;
namespace Tazzker.Client.Services
{
	public class ModelMethods
	{
		private readonly ILocalDb<TaskModel> TaskDb;
		private readonly ILocalDb<SublistModel> SublistDb;
		private readonly ILocalDb<ListModel> ListDb;
		private readonly ILocalDb<NoteModel> NoteDb;


		public ModelMethods(ILocalDb<TaskModel> taskDb, ILocalDb<SublistModel> sublistDb, ILocalDb<ListModel> listDb, ILocalDb<NoteModel> noteDb)
		{
			TaskDb = taskDb;
			SublistDb = sublistDb;
			ListDb = listDb;
			NoteDb = noteDb;
		}

		#region Tasks

		public async Task AddTask(Guid? sublistId = null, DateTime? dueDay = null)
		{

			var task = new TaskModel();
			if (sublistId != null) task.SublistId = sublistId;
			if (dueDay != null) task.DueTime = dueDay;
			task.Order = await GetLastOrder() + 0.01f;
			await TaskDb.AddOrUpdateAsync(task);
		}

		private async Task<float> GetLastOrder()
		{
			var tasks = await TaskDb.GetAllAsync();

			if (tasks == null || tasks.Count == 0) return 0f;

			var lastOrder = tasks.Max(x => x.Order);
			return lastOrder;

		}

		public async Task<List<TaskModel>> GetAllTasks() => await TaskDb.GetAllAsync();
		public async Task SaveTask(TaskModel task) => await TaskDb.AddOrUpdateAsync(task);
		public async Task DeleteTask(Guid id) => await TaskDb.SoftDeleteAsync(id);

		#endregion


		#region Sublists

		public async Task AddSublist(Guid listId)
		{
			var sub = new SublistModel { ListId = listId };
			await SublistDb.AddOrUpdateAsync(sub);
		}

		public async Task<List<SublistModel>> GetAllSublists() => await SublistDb.GetAllAsync();
		public async Task SaveSublist(SublistModel sublist) => await SublistDb.AddOrUpdateAsync(sublist);
		public async Task DeleteSublist(Guid id) => await SublistDb.SoftDeleteAsync(id);

		#endregion


		#region Lists

		public async Task<Guid> AddList()
		{
			var list = new ListModel();
			await ListDb.AddOrUpdateAsync(list);
			return list.Id;
		}

		public async Task<ListModel?> GetListById(Guid listId) => await ListDb.GetByIdAsync(listId);
		public async Task<List<ListModel>> GetAllLists() => await ListDb.GetAllAsync();
		public async Task DeleteList(Guid id) => await ListDb.SoftDeleteAsync(id);
		public async Task SaveList(ListModel list) => await ListDb.AddOrUpdateAsync(list);

		#endregion

		#region Notes

		public async Task<Guid> AddNote()
		{
			var note = new NoteModel();
			await NoteDb.AddOrUpdateAsync(note);
			return note.Id;
		}

		public async Task<NoteModel?> GetNoteById(Guid noteId) => await NoteDb.GetByIdAsync(noteId);
		public async Task<List<NoteModel>> GetAllNotes() => await NoteDb.GetAllAsync();
		public async Task SaveNote(NoteModel note) => await NoteDb.AddOrUpdateAsync(note);
		public async Task DeleteNote(Guid id) => await NoteDb.SoftDeleteAsync(id);
		#endregion
	}
}
