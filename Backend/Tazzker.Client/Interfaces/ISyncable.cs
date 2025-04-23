namespace Tazzker.Client.Interfaces
{
    public interface ISyncable
    {
        public bool IsSynced { get; set; }
        public bool IsDeleted { get; set; }
    }
}
