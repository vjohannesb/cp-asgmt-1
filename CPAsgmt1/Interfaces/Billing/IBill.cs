using CPAsgmt1.Interfaces.Kennel;

namespace CPAsgmt1.Interfaces.Billing
{
    internal interface IBill
    {
        public Guid BillId { get; }
        public IEnumerable<IService>? Services { get; set; }
        public decimal TotalCost { get; set; }
        public IBillable Recipient { get; set; }
        public bool Paid { get; set; }

        public IBill CloseBill();
    }
}
