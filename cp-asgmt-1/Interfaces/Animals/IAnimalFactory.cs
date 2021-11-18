using cp_asgmt_1.Interfaces.Customers;

namespace cp_asgmt_1.Interfaces.Animals
{
    internal interface IAnimalFactory
    {
        public IAnimal CreateAnimal(string name, ICustomer owner);
    }
}
