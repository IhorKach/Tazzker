namespace Tazzker.Client.Interfaces
{
    public interface ISyncable
    {
        public Guid Id { get; set; }
        public bool IsSynced { get; set; }
        public bool PermDeleted { get; set; }
    }
}
