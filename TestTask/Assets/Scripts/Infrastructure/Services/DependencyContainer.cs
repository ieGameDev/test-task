namespace Assets.Scripts.Infrastructure.Services
{
    public class DependencyContainer
    {
        private static DependencyContainer _instance;
        public static DependencyContainer Container => _instance ?? (_instance = new DependencyContainer());

        public void RegisterSingle<TService>(TService implementation) where TService : IService =>
            Implementation<TService>.ServiceInstance = implementation;

        public TService Single<TService>() where TService : IService =>
            Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}
