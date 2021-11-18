using cp_asgmt_1.Interfaces.Kennel;

namespace cp_asgmt_1.Models.Kennel
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
        {
            return _createKennel(name, price);
        }

        public IService CreateService(string name, decimal price)
        {
            return _createService(name, price);
        }
    }
}
