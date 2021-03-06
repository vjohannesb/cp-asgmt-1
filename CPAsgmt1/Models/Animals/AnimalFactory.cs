using CPAsgmt1.Interfaces.Animals;
using CPAsgmt1.Interfaces.Customers;

namespace CPAsgmt1.Models.Animals
{
    internal class AnimalFactory : IAnimalFactory
    {
        private readonly Func<string, ICustomer, IAnimal> _createAnimal;
        public AnimalFactory(Func<string, ICustomer, IAnimal> createAnimal)
        {
            _createAnimal = createAnimal;
        }

        public IAnimal CreateAnimal(string name, ICustomer owner)
            => _createAnimal(name, owner);
    }
}
