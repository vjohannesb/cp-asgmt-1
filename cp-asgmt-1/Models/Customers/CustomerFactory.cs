using cp_asgmt_1.Interfaces.Customers;

namespace cp_asgmt_1.Models.Customers
{
    internal class CustomerFactory : ICustomerFactory
    {
        private readonly Func<string, ICustomer> _createCustomer;

        public CustomerFactory(Func<string, ICustomer> createCustomer)
        {
            _createCustomer = createCustomer;
        }

        public ICustomer CreateCustomer(string name)
        {
            return _createCustomer(name);
        }
    }
}
