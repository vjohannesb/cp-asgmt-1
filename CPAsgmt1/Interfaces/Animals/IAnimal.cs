using CPAsgmt1.Interfaces.Billing;
using CPAsgmt1.Interfaces.Customers;

namespace CPAsgmt1.Interfaces.Animals
{
    internal interface IAnimal
    {
        public Guid AnimalId { get; }
        public string Name { get; set; }
        public ICustomer Owner { get; set; }
        public IBill? Bill { get; set; } 
        public bool IsAtKennel { get; set; }
    }
}
