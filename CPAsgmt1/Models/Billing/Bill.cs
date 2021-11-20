using CPAsgmt1.Interfaces.Animals;
using CPAsgmt1.Interfaces.Billing;
using CPAsgmt1.Interfaces.Kennel;

namespace CPAsgmt1.Models.Billing
{
    internal class Bill : IBill
    {
        public Bill(IAnimal recipient, decimal price, IEnumerable<IService>? services)
        {
            BillId = Guid.NewGuid();
            TotalCost = price;
            Recipient = recipient;
            Services = services;
            Paid = false;
        }

        public Guid BillId { get; }
        public decimal TotalCost { get; set; }
        public IAnimal Recipient { get; set; }
        public IEnumerable<IService>? Services { get; set; }
        public bool Paid { get; private set; }

        public void Close()
            => TotalCost += Services?.Select(s => s.Price).Sum() ?? 0;

        public void Pay()
        {
            Recipient.Bill = null;
            Paid = true;
        }
    }
}
