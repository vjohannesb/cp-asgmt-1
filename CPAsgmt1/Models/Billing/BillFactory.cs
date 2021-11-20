using CPAsgmt1.Interfaces.Animals;
using CPAsgmt1.Interfaces.Billing;
using CPAsgmt1.Interfaces.Kennel;

namespace CPAsgmt1.Models.Billing
{
    internal class BillFactory : IBillFactory
    {
        private readonly Func<IAnimal, decimal, IEnumerable<IService>?, IBill> _createBill;

        public BillFactory(Func<IAnimal, decimal, IEnumerable<IService>?, IBill> createBill)
        {
            _createBill = createBill;
        }

        public IBill CreateBill(IAnimal recipient, decimal price, IEnumerable<IService>? services)
        {
            var bill = _createBill(recipient, price, services);
            recipient.Bill = bill;
            return bill;
        }
    }
}
