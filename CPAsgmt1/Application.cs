using CPAsgmt1.Interfaces;
using CPAsgmt1.Interfaces.Animals;
using CPAsgmt1.Interfaces.Billing;
using CPAsgmt1.Interfaces.Customers;
using CPAsgmt1.Interfaces.IO;
using CPAsgmt1.Interfaces.Kennel;
using CPAsgmt1.Interfaces.Menu;

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

        private IMenu _mainMenu;

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

            _io.Init();
            AddMockData(_kennel, _customerFactory, _animalFactory, _kennelFactory);

            _mainMenu = CreateMainMenu();
        }

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
            int i = 0;
            _menuFactory.InitMenu(_kennel.Name);
            _menuFactory.AddMenuItem(++i, "Register customer", AddCustomer);
            _menuFactory.AddMenuItem(++i, "Register animal", AddAnimal);
            _menuFactory.AddMenuItem(++i, "View customers", ViewCustomers);
            _menuFactory.AddMenuItem(++i, "View animals", ViewAnimals);
            _menuFactory.AddMenuItem(++i, "View animals at kennel", ViewAnimalsAtKennel);
            _menuFactory.AddMenuItem(++i, "Check in animal", CheckInAnimal);
            _menuFactory.AddMenuItem(++i, "Check out animal", CheckOutAnimal);
            return _menuFactory.CreateMenu("Exit", () => Environment.Exit(0));
        }

        public void Init() { }

        public void Run()
        {
            var keys = _mainMenu.MenuItems.Select(mi => mi.Name);
            var items = _mainMenu.MenuItems;

            while (true)
            {
                var selection = _io.GetSelection(_mainMenu, "Enter selection: ");
                if (selection == null) return;

                selection.Run?.Invoke();
            }
        }

        private void Pause(string? message = null)
        {
            if (!string.IsNullOrEmpty(message))
                _io.WriteLine(message);
            _io.Write("Press any key to continue.");
            _io.ReadKey(false);
        }

        private void AddAnimal()
        {
            string? name;
            name = _io.GetAnswer("Register animal", "Name: ", true);
            if (string.IsNullOrEmpty(name)) return;

            var customers = _kennel.GetCustomers();
            var customerNames = customers.Select(c => c.Name);
            var owner = _io.GetSelection("Customers", "Select owner: ", customerNames, customers);
            if (owner == null) return;

            _kennel.AddAnimal(_animalFactory.CreateAnimal(name, owner));
            Pause($"{name} has been registered!");
        }

        private void AddCustomer()
        {
            string? name;
            name = _io.GetAnswer("Register customer", "Name: ", true);
            if (string.IsNullOrEmpty(name)) return;

            _kennel.AddCustomer(_customerFactory.CreateCustomer(name));

            Pause($"{name} has been registered!");
        }

        private void ViewAnimals()
        {
            var items = _kennel.GetAnimals().Select(a => a.Name);
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
            var keys = animals.Select(a => a.Name);

            var animal = _io.GetSelection("Animals", "Select animal: ", keys, animals);
            if (animal == null) return;

            List<IService> selectedServices = new();
            var availableServices = _kennel.GetServices();
            foreach (var service in availableServices)
            {
                var prompt = $"Would you like to add {service.Name} for only {service.Price}kr? [Y/N] ";
                var answer = _io.GetAnswer("Services", prompt, true);
                if (string.IsNullOrEmpty(answer)) break;

                if (answer.ToLower().Contains('y'))
                    selectedServices.Add(service);
            }


            animal.IsAtKennel = true;
            var message = $"{animal.Name} has been checked in! Selected services: ";
            message += string.Join(", ", selectedServices.Select(s => s.Name));

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

            animal.IsAtKennel = true;
            var message = $"{animal.Name} has been checked out! Total bill: ";

            _io.WriteLine(message);
            _io.ListItems($"Bill for {animal.Name}", new List<string>(), false);
            _io.WriteLine("Total: 19.99kr");

            Pause();
        }
    }
}
