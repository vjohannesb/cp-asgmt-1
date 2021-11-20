using CPAsgmt1.Interfaces.Animals;
using CPAsgmt1.Interfaces.Kennel;

namespace CPAsgmt1.Interfaces.Billing
{
    internal interface IBillFactory
    {
        public IBill CreateBill(IAnimal recipient, decimal price, IEnumerable<IService>? services);
    }
}
