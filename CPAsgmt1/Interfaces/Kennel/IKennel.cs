using CPAsgmt1.Interfaces.Animals;
using CPAsgmt1.Interfaces.Customers;

namespace CPAsgmt1.Interfaces.Kennel
{
    internal interface IKennel
    {
        Guid KennelId { get; }
        string Name { get; set; }
        decimal CostPerDay { get; set; }
        IEnumerable<IAnimal> Animals { get; set; }
        IEnumerable<ICustomer> Customers { get; set; }
        IEnumerable<IService> Services { get; set; }

        IEnumerable<IAnimal> GetAnimals();
        IEnumerable<IAnimal> GetAnimalsAtKennel();
        IEnumerable<ICustomer> GetCustomers();
        IEnumerable<IService> GetServices();
        void AddAnimal(IAnimal animal);
        void AddCustomer(ICustomer customer);
        void AddService(IService service);
    }
}