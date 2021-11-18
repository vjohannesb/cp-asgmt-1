using CPAsgmt1.Interfaces.Billing;
using CPAsgmt1.Interfaces.Kennel;

namespace CPAsgmt1.Models.Billing
{
    internal class BillFactory : IBillFactory
    {
        private readonly Func<IBillable, decimal, IEnumerable<IService>?, IBill> _createBill;
        public BillFactory(Func<IBillable, decimal, IEnumerable<IService>?, IBill> createBill)
        {
            _createBill = createBill;
        }
        public IBill CreateBill(IBillable recipient, decimal price, IEnumerable<IService>? services)
        {
            return _createBill(recipient, price, services);
        }
    }
}
