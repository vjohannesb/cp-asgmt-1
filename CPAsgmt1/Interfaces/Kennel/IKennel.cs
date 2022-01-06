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

        void AddAnimal();
        void CheckInAnimal();
        void CheckOutAnimal();
        void ViewAnimals();
        void ViewAnimalsAtKennel();
        IEnumerable<IAnimal> GetAnimals();
        IEnumerable<IAnimal> GetAnimalsAtKennel();

        void AddCustomer();
        void ViewCustomers();
        IEnumerable<ICustomer> GetCustomers();

        void AddService();
        IEnumerable<IService> GetServices();


    }
}