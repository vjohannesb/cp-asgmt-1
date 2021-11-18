using cp_asgmt_1.Interfaces.Animals;
using cp_asgmt_1.Interfaces.Customers;

namespace cp_asgmt_1.Models.Animals
{
    internal class Animal : IAnimal
    {
        public Animal(string name, ICustomer owner)
        {
            AnimalId = Guid.NewGuid();
            Name = name;
            Owner = owner;
            IsAtKennel = false;
        }

        public Guid AnimalId { get; }
        public string Name { get; set; }
        public ICustomer Owner { get; set; }
        public bool IsAtKennel { get; set; }
    }
}
