using CPAsgmt1.Interfaces.Animals;
using CPAsgmt1.Interfaces.Kennel;

namespace CPAsgmt1.Interfaces.Billing
{
    internal interface IBill
    {
        public Guid BillId { get; }
        public IEnumerable<IService>? Services { get; set; }
        public decimal TotalCost { get; set; }
        public IAnimal Recipient { get; set; }
        public bool Paid { get; }

        public void Close();
        public void Pay();
    }
}
