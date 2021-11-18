using CPAsgmt1.Interfaces.Billing;
using CPAsgmt1.Interfaces.Kennel;

namespace CPAsgmt1.Models.Billing
{
    internal class Bill : IBill
    {
        public Bill(decimal totalCost, IBillable recipient, IEnumerable<IService>? services)
        {
            BillId = Guid.NewGuid();
            TotalCost = totalCost;
            Recipient = recipient;
            Services = services;
            Paid = false;
        }

        public Guid BillId { get; }
        public decimal TotalCost { get; set; }
        public IBillable Recipient { get; set; }
        public IEnumerable<IService>? Services { get; set; }
        public bool Paid { get; set; }

        public IBill CloseBill()
        {
            TotalCost += Services?.Select(s => s.Price).Sum() ?? 0;
            return this;
        }
    }
}
