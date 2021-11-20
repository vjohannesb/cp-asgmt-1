using CPAsgmt1.Interfaces;
using CPAsgmt1.Interfaces.Animals;
using CPAsgmt1.Interfaces.Billing;
using CPAsgmt1.Interfaces.Customers;
using CPAsgmt1.Interfaces.IO;
using CPAsgmt1.Interfaces.Kennel;
using CPAsgmt1.Interfaces.Menu;
using System.Linq;

namespace CPAsgmt1
{
    internal class Application : IApplication
    {
        private readonly IKennel _kennel;

        private readonly IIO _io;

        private readonly IMenuFactory _menuFactory;
        private readonly IKennelFactory _kennelFactory;
        private readonly IBillFactory _billFactory;
        private readonly IAnimalFactory _animalFactory;
        private readonly ICustomerFactory _customerFactory;

        private readonly IMenu _mainMenu;

        public Application(IIO io, IMenuFactory menuFactory,
            IKennelFactory kennelFactory, IAnimalFactory animalFactory,
            IBillFactory billFactory, ICustomerFactory customerFactory)
        {
            _io = io;

            _menuFactory = menuFactory;
            _kennelFactory = kennelFactory;
            _animalFactory = animalFactory;
            _billFactory = billFactory;
            _customerFactory = customerFactory;

            _kennel = _kennelFactory.CreateKennel("Floofers", 19.99m);
            _mainMenu = CreateMainMenu();

            Init();
        }
        public void Init()
        {
            _io.Init();
            AddMockData(_kennel, _customerFactory, _animalFactory, _kennelFactory);
        }

        public void Run()
        {
            while (true)
            {
                var selection = _io.GetSelection(_mainMenu, "Enter selection: ", "Exit");
                if (selection == null) return;

                selection.Run?.Invoke();
            }
        }

        // For assignment / examination
        private static void AddMockData(IKennel k, ICustomerFactory cf,
            IAnimalFactory af, IKennelFactory kf)
        {
            k.AddCustomer(cf.CreateCustomer("Anna Andersson"));
            k.AddCustomer(cf.CreateCustomer("Bengt Bengtsson"));
            k.AddCustomer(cf.CreateCustomer("Carl Carlsson"));
            k.AddAnimal(af.CreateAnimal("Apollo", k.Customers.ElementAt(0)));
            k.AddAnimal(af.CreateAnimal("Bobbos", k.Customers.ElementAt(1)));
            k.AddAnimal(af.CreateAnimal("Caesar", k.Customers.ElementAt(2)));
            k.AddService(kf.CreateService("Grooming", 9.99m));
            k.AddService(kf.CreateService("Mani-pedi", 4.99m));
        }

        private IMenu CreateMainMenu()
        {
            _menuFactory.InitMenu(_kennel.Name);
            _menuFactory.AddMenuItem("Register customer", AddCustomer);
            _menuFactory.AddMenuItem("Register animal", AddAnimal);
            _menuFactory.AddMenuItem("View customers", ViewCustomers);
            _menuFactory.AddMenuItem("View animals", ViewAnimals);
            _menuFactory.AddMenuItem("View animals at kennel", ViewAnimalsAtKennel);
            _menuFactory.AddMenuItem("Check in animal", CheckInAnimal);
            _menuFactory.AddMenuItem("Check out animal", CheckOutAnimal);
            return _menuFactory.CreateMenu();
        }

        private void Pause(string? message = null)
        {
            if (!string.IsNullOrEmpty(message))
                _io.WriteLine(message);
            _io.Write("Press any key to continue.");
            _io.ReadKey(false);
        }

        private void AddCustomer()
        {
            string? name;
            name = _io.GetAnswer("Register customer", "Name: ", true);
            if (string.IsNullOrEmpty(name)) return;

            _kennel.AddCustomer(_customerFactory.CreateCustomer(name));

            Pause($"{name} has been registered!");
        }

        private void AddAnimal()
        {
            string? name;
            name = _io.GetAnswer("Register animal", "Name: ", true);
            if (name == null) return;

            var customers = _kennel.GetCustomers();
            var customerNames = customers.Select(c => c.Name);
            var owner = _io.GetSelection("Customers", "Select owner: ", customerNames, customers, "Cancel");
            if (owner == null) return;

            _kennel.AddAnimal(_animalFactory.CreateAnimal(name, owner));
            Pause($"{name} has been registered!");
        }

        private void ViewAnimals()
        {
            var items = _kennel.GetAnimals().Select(a => $"{a.Name} ({a.Owner.Name})");
            _io.ListItems("Animals", items, true);
            Pause();
        }

        private void ViewCustomers()
        {
            var items = _kennel.GetCustomers().Select(c => c.Name);
            _io.ListItems("Customers", items, true);
            Pause();
        }

        private void ViewAnimalsAtKennel()
        {
            var items = _kennel.GetAnimals().Where(a => a.IsAtKennel).Select(a => a.Name);
            _io.ListItems("Animals at kennel", items, true);
            Pause();
        }

        private void CheckInAnimal()
        {
            var animals = _kennel.GetAnimals().Where(a => !a.IsAtKennel);
            if (!animals.Any())
            {
                Pause("There are no registered animals that are not at the kennel at the moment.");
                return;
            }

            var keys = animals.Select(a => a.Name);

            var animal = _io.GetSelection("Animals", "Select animal: ", keys, animals, "Cancel");
            if (animal == null) return;

            if (animal.Bill != null && !animal.Bill.Paid)
            {
                Pause("You cannot check in an animal with an unpaid bill.");
                return;
            }

            List<IService> selectedServices = new();
            var availableServices = _kennel.GetServices();
            while (availableServices.Any())
            {
                var serviceNames = availableServices.Select(s => s.Name);
                var service = _io.GetSelection("Services", "Select service: ", serviceNames, availableServices, "None");
                if (service == null) break;

                selectedServices.Add(service);
                availableServices = availableServices.Where(s => s != service);
            }

            _billFactory.CreateBill(animal, _kennel.CostPerDay, selectedServices);

            animal.IsAtKennel = true;
            var message = $"{animal.Name} has been checked in!";
            if (selectedServices.Any())
            {
                message += " Extra services: ";
                message += string.Join(", ", selectedServices.Select(s => s.Name));
            }

            Pause(message);
        }

        private void CheckOutAnimal()
        {
            var animals = _kennel.GetAnimals().Where(a => a.IsAtKennel);
            if (!animals.Any())
            {
                Pause("No animals checked in.");
                return;
            }

            var keys = animals.Select(a => a.Name);
            var animal = _io.GetSelection("Animals at kennel", "Select animal to check out: ", keys, animals, "Cancel");
            if (animal == null) return;

            if (animal.Bill == null)
            {
                _io.WriteLine($"Something went wrong. There is no bill registered for {animal.Name}.");
                return;
            }

            animal.IsAtKennel = false;
            animal.Bill.Close();

            List<string> billedServices = new() { $"Stay at {_kennel.Name} - {_kennel.CostPerDay}kr" };
            if (animal.Bill.Services != null && animal.Bill.Services.Any())
            {
                var services = animal.Bill.Services.Select(s => $"{s.Name} - {s.Price}kr");
                billedServices.AddRange(services);
            }

            _io.WriteLine($"{animal.Name} has been checked out!", true);
            _io.ListItems($"Bill for {animal.Name}", billedServices, false);
            _io.WriteLine($"Total: {animal.Bill.TotalCost}kr");
            _io.WriteLine("");

            Pause("Your bill is automatically paid.");
            animal.Bill.Pay();
        }
    }
}
