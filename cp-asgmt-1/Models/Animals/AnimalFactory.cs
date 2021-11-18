using cp_asgmt_1.Interfaces.Animals;
using cp_asgmt_1.Interfaces.Customers;

namespace cp_asgmt_1.Models.Animals
{
    internal class AnimalFactory : IAnimalFactory
    {
        private readonly Func<string, ICustomer, IAnimal> _createAnimal;
        public AnimalFactory(Func<string, ICustomer, IAnimal> createAnimal)
        {
            _createAnimal = createAnimal;
        }

        public IAnimal CreateAnimal(string name, ICustomer owner)
        {
            return _createAnimal(name, owner);
        }
    }
}
