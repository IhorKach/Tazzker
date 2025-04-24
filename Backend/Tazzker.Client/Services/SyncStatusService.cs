namespace Tazzker.Client.Services
{
    public class SyncStatusService
    {
        public event Action? OnChange;

        private SyncState _status = SyncState.Idle;
        public SyncState Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnChange?.Invoke();
                }
            }
        }
    }

    public enum SyncState
    {
        Idle,
        Syncing,
        Synced,
        Error
    }
}
