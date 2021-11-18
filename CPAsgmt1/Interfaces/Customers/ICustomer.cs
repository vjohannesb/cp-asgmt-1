using CPAsgmt1.Interfaces.Billing;

namespace CPAsgmt1.Interfaces.Customers
{
    internal interface ICustomer : IBillable
    {
        public Guid CustomerId { get; }
        public string Name { get; set; }
    }
}
