using CPAsgmt1.Interfaces.Animals;
using CPAsgmt1.Interfaces.Billing;
using CPAsgmt1.Interfaces.Customers;
using CPAsgmt1.Interfaces.IO;
using CPAsgmt1.Interfaces.Kennel;

namespace CPAsgmt1.Models.Kennel
{
    internal class Kennel : IKennel
    {
        private readonly IIO _io;
        private readonly IBillFactory _billFactory;
        private readonly IAnimalFactory _animalFactory;
        private readonly ICustomerFactory _customerFactory;
        private readonly IServiceFactory _serviceFactory;

        public Kennel(string name, decimal costPerDay, IIO io, IBillFactory billFactory,
            IAnimalFactory animalFactory, ICustomerFactory customerFactory, IServiceFactory serviceFactory)
        {
            Name = name;
            CostPerDay = costPerDay;

            _io = io;
            _billFactory = billFactory;
            _animalFactory = animalFactory;
            _customerFactory = customerFactory;
            _serviceFactory = serviceFactory;

            // For assignment / examination
            AddMockData();
        }

        public string Name { get; set; }
        public decimal CostPerDay { get; set; }
        public Guid KennelId { get; } = Guid.NewGuid();
        public IEnumerable<IAnimal> Animals { get; set; } = new List<IAnimal>();
        public IEnumerable<ICustomer> Customers { get; set; } = new List<ICustomer>();
        public IEnumerable<IService> Services { get; set; } = new List<IService>();

        //! ANIMALS
        public void AddAnimal()
        {
            var name = _io.GetAnswer("Register animal", "Name: ", true);
            if (name == null) return;

            IEnumerable<ICustomer> customers = GetCustomers();
            IEnumerable<string> customerNames = customers.Select(c => c.Name);
            ICustomer? owner = _io.GetSelection("Customers", "Select owner: ", customerNames, customers, "Cancel");
            if (owner == null) return;

            IAnimal animal = _animalFactory.CreateAnimal(name, owner);
            Animals = Animals.Append(animal);
            Pause($"{name} has been registered!");
        }

        public void CheckInAnimal()
        {
            IEnumerable<IAnimal> animals = GetAnimals().Where(a => !a.IsAtKennel);
            if (!animals.Any())
            {
                Pause("There are no registered animals that are not at the kennel at the moment.");
                return;
            }

            IEnumerable<string> keys = animals.Select(a => a.Name);

            IAnimal? animal = _io.GetSelection("Animals", "Select animal: ", keys, animals, "Cancel");
            if (animal == null) return;

            if (animal.Bill != null && !animal.Bill.Paid)
            {
                Pause("You cannot check in an animal with an unpaid bill.");
                return;
            }

            List<IService> selectedServices = new();
            IEnumerable<IService> availableServices = GetServices();
            while (availableServices.Any())
            {
                IEnumerable<string>? serviceNames = availableServices.Select(s => s.Name);
                IService? service = _io.GetSelection("Services", "Select service: ", serviceNames, availableServices, "None");
                if (service == null) break;

                selectedServices.Add(service);
                availableServices = availableServices.Where(s => s != service);
            }

            _billFactory.CreateBill(animal, CostPerDay, selectedServices);

            animal.IsAtKennel = true;
            var message = $"{animal.Name} has been checked in!";
            if (selectedServices.Any())
            {
                message += " Extra services: ";
                message += string.Join(", ", selectedServices.Select(s => s.Name));
            }

            Pause(message);
            animal.IsAtKennel = true;
        }

        public void CheckOutAnimal()
        {

            IEnumerable<IAnimal>? animals = GetAnimals().Where(a => a.IsAtKennel);
            if (!animals.Any())
            {
                Pause("No animals checked in.");
                return;
            }

            IEnumerable<string>? keys = animals.Select(a => a.Name);
            IAnimal? animal = _io.GetSelection("Animals at kennel", "Select animal to check out: ", keys, animals, "Cancel");
            if (animal == null) return;

            if (animal.Bill == null)
            {
                _io.WriteLine($"Something went wrong. There is no bill registered for {animal.Name}.");
                return;
            }

            animal.IsAtKennel = false;
            animal.Bill.Close();

            List<string> billedServices = new() { $"Stay at {Name} - {CostPerDay}kr" };
            if (animal.Bill.Services != null && animal.Bill.Services.Any())
            {
                IEnumerable<string>? services = animal.Bill.Services.Select(s => $"{s.Name} - {s.Price}kr");
                billedServices.AddRange(services);
            }

            _io.WriteLine($"{animal.Name} has been checked out!", true);
            _io.ListItems($"Bill for {animal.Name}", billedServices, false);
            _io.WriteLine($"Total: {animal.Bill.TotalCost}kr");
            _io.WriteLine("");

            // If you remove this, you need to implement a PayBills-function,
            // otherwise a previously checked-in animal cannot be checked in again.
            // Is, however, outside assignment scope.
            animal.Bill.Pay();
            animal.IsAtKennel = false;
            Pause("Your bill is automatically paid.");
        }

        public void ViewAnimals()
        {
            IEnumerable<string>? items = GetAnimals()
                .Select(a => $"{a.Name} ({a.Owner.Name})");

            _io.ListItems("Animals", items, true);
            Pause();
        }

        public void ViewAnimalsAtKennel()
        {
            IEnumerable<string>? items = GetAnimals()
                .Where(a => a.IsAtKennel)
                .Select(a => $"{a.Name} ({a.Owner.Name})");

            _io.ListItems("Animals at kennel", items, true);
            Pause();
        }

        public IEnumerable<IAnimal> GetAnimals()
            => Animals;

        public IEnumerable<IAnimal> GetAnimalsAtKennel()
            => Animals.Where(a => a.IsAtKennel);

        //! CUSTOMERS
        public void AddCustomer()
        {
            var name = _io.GetAnswer("Register customer", "Name: ", true);
            if (string.IsNullOrEmpty(name)) return;

            var customer = _customerFactory.CreateCustomer(name);
            Customers = Customers.Append(customer);

            Pause($"{name} has been registered!");
        }

        public void ViewCustomers()
        {
            IEnumerable<string>? items = GetCustomers().Select(c => c.Name);
            _io.ListItems("Customers", items, true);
            Pause();
        }

        public IEnumerable<ICustomer> GetCustomers()
            => Customers;

        public IEnumerable<IService> GetServices()
            => Services;

        //! SERVICES
        public void AddService()
        {
            var name = _io.GetAnswer("Register service", "Name: ", true);
            if (string.IsNullOrEmpty(name)) return;

            var price = _io.GetAnswer("Price of service", "Price: ", true);
            if (string.IsNullOrEmpty (price)) return;
            if (!decimal.TryParse(price, out var result)) return;

            IService service = _serviceFactory.CreateService(name, result);
            Services = Services.Append(service);

            Pause($"{name} has been registered!");
        }

        //! HELPERS
        private void Pause(string? message = null)
        {
            if (!string.IsNullOrEmpty(message))
                _io.WriteLine(message);
            _io.Write("Press any key to continue.");
            _io.ReadKey(false);
        }

        // For examination / assignment
        void AddMockData()
        {
            Customers = new List<ICustomer>()
            {
                _customerFactory.CreateCustomer("Anna Andersson"),
                _customerFactory.CreateCustomer("Bengt Bengtsson"),
                _customerFactory.CreateCustomer("Carl Carlsson")
            };
            Animals = new List<IAnimal>()
            {
                _animalFactory.CreateAnimal("Apollo", Customers.ElementAt(0)),
                _animalFactory.CreateAnimal("Bobbos", Customers.ElementAt(1)),
                _animalFactory.CreateAnimal("Caesar", Customers.ElementAt(2)),
            };
            Services = new List<IService>()
            {
                _serviceFactory.CreateService("Grooming", 9.99m),
                _serviceFactory.CreateService("Mani-pedi", 4.99m)
            };
        }
    }
}
