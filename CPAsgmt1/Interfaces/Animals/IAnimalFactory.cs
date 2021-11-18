using CPAsgmt1.Interfaces.Customers;

namespace CPAsgmt1.Interfaces.Animals
{
    internal interface IAnimalFactory
    {
        public IAnimal CreateAnimal(string name, ICustomer owner);
    }
}
