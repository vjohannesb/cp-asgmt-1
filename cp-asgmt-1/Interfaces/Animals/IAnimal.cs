using cp_asgmt_1.Interfaces.Customers;

namespace cp_asgmt_1.Interfaces.Animals
{
    internal interface IAnimal
    {
        public Guid AnimalId { get; }
        public string Name { get; set; }
        public ICustomer Owner { get; set; }
        public bool IsAtKennel { get; set; }
    }
}
