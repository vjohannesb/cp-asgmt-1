using CPAsgmt1.Interfaces.Kennel;

namespace CPAsgmt1.Models.Kennel
{
    internal class KennelFactory : IKennelFactory
    {
        private readonly Func<string, decimal, IKennel> _createKennel;
        private readonly Func<string, decimal, IService> _createService;

        public KennelFactory(Func<string, decimal, IKennel> createKennel,
            Func<string, decimal, IService> createService)
        {
            _createKennel = createKennel;
            _createService = createService;
        }

        public IKennel CreateKennel(string name, decimal price) 
            => _createKennel(name, price);

        public IService CreateService(string name, decimal price) 
            => _createService(name, price);
    }
}
