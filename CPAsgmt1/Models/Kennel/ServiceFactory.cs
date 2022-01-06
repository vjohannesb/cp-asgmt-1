using CPAsgmt1.Interfaces.Kennel;

namespace CPAsgmt1.Models.Kennel
{
    internal class ServiceFactory : IServiceFactory
    {
        private readonly Func<string, decimal, IService> _createService;

        public ServiceFactory(Func<string, decimal, IService> createService)
        {
            _createService = createService;
        }
        public IService CreateService(string name, decimal price)
            => _createService(name, price);
    }
}
