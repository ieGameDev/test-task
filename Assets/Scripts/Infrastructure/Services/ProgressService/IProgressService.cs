using Assets.Scripts.Data;

namespace Assets.Scripts.Infrastructure.Services.ProgressService
{
    public interface IProgressService : IService
    {
        Progress Progress { get; set; }
    }
}