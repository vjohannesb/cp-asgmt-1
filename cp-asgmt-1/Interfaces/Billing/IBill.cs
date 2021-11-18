using cp_asgmt_1.Interfaces.Kennel;

namespace cp_asgmt_1.Interfaces.Billing
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
