using CPAsgmt1.Interfaces.Customers;

namespace CPAsgmt1.Models.Customers
{
    internal class CustomerFactory : ICustomerFactory
    {
        private readonly Func<string, ICustomer> _createCustomer;

        public CustomerFactory(Func<string, ICustomer> createCustomer)
        {
            _createCustomer = createCustomer;
        }

        public ICustomer CreateCustomer(string name)
            => _createCustomer(name);
    }
}
