using cp_asgmt_1.Interfaces.Customers;

namespace cp_asgmt_1.Models.Customers
{
    internal class Customer : ICustomer
    {
        public Customer(string name)
        {
            CustomerId = Guid.NewGuid();
            Name = name;
            IdNumber = "990101-1234";
            StreetAddress = "Gatan 1";
            City = "Staden";
            ZipCode = "123 45";
            PhoneNumber = "0701234567";
            Email = "email@email.com";
        }

        public Guid CustomerId { get; }
        public string Name { get; set; }
        public string IdNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
