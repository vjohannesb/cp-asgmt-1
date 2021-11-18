namespace CPAsgmt1.Interfaces.Customers
{
    internal interface ICustomerFactory
    {
        public ICustomer CreateCustomer(string name);
    }
}
