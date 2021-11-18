using cp_asgmt_1.Interfaces.Animals;
using cp_asgmt_1.Interfaces.Customers;
using cp_asgmt_1.Interfaces.Kennel;

namespace cp_asgmt_1.Models.Kennel
{
    internal class Kennel : IKennel
    {
        public Kennel(string name, decimal costPerDay)
        {
            KennelId = Guid.NewGuid();
            Name = name;
            CostPerDay = costPerDay;
            Animals = new List<IAnimal>();
            Customers = new List<ICustomer>();
            Services = new List<IService>();
        }

        public Guid KennelId { get; }
        public string Name { get; set; }
        public decimal CostPerDay { get; set; }
        public IEnumerable<IAnimal> Animals { get; set; }
        public IEnumerable<ICustomer> Customers { get; set; }
        public IEnumerable<IService> Services { get; set; }


        public IEnumerable<ICustomer> GetCustomers()
            => Customers;
        public IEnumerable<IAnimal> GetAnimals()
            => Animals;
        public IEnumerable<IAnimal> GetAnimalsAtKennel()
            => Animals.Where(a => a.IsAtKennel);
        public IEnumerable<IService> GetServices()
            => Services;

        public static void CheckInAnimal(IAnimal animal)
            => animal.IsAtKennel = true;
        public static void CheckOutAnimal(IAnimal animal)
            => animal.IsAtKennel = false;

        public void AddAnimal(IAnimal animal)
            => Animals = Animals.Append(animal);
        public void AddCustomer(ICustomer customer)
            => Customers = Customers.Append(customer);
        public void AddService(IService service)
            => Services = Services.Append(service);
    }
}
