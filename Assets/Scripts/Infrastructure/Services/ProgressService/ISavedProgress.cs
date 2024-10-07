using Assets.Scripts.Data;

namespace Assets.Scripts.Infrastructure.Services.ProgressService
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(Progress progress);
    }

    public interface ISavedProgressReader
    {
        void LoadProgress(Progress progress);
    }
}
