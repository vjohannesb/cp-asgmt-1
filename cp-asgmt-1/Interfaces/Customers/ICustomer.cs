using cp_asgmt_1.Interfaces.Billing;

namespace cp_asgmt_1.Interfaces.Customers
{
    internal interface ICustomer : IBillable
    {
        public Guid CustomerId { get; }
        public string Name { get; set; }
    }
}
