using cp_asgmt_1.Interfaces;
using cp_asgmt_1.Interfaces.Animals;
using cp_asgmt_1.Interfaces.Billing;
using cp_asgmt_1.Interfaces.Customers;
using cp_asgmt_1.Interfaces.IO;
using cp_asgmt_1.Interfaces.Kennel;
using cp_asgmt_1.Interfaces.Menu;

namespace cp_asgmt_1
{
    internal class Application : IApplication
    {
        private readonly IKennel _kennel;

        private readonly IInput _input;
        private readonly IOutput _output;

        private readonly IMenuFactory _menuFactory;
        private readonly IKennelFactory _kennelFactory;
        private readonly IBillFactory _billFactory;
        private readonly IAnimalFactory _animalFactory;
        private readonly ICustomerFactory _customerFactory;

        private IMenu _mainMenu;
        private IMenu AnimalMenu => CreateMenu("Animals", _kennel.GetAnimals().Select(a => a.Name));
        private IMenu CustomerMenu => CreateMenu("Customers", _kennel.GetCustomers().Select(c => c.Name));
        private IMenu ServiceMenu => CreateMenu("Services", _kennel.GetServices().Select(s => s.Name));

        public Application(IInput input, IOutput output,
            IMenuFactory menuFactory, IKennelFactory kennelFactory,
            IAnimalFactory animalFactory, IBillFactory billFactory,
            ICustomerFactory customerFactory)
        {
            _input = input;
            _output = output;

            _menuFactory = menuFactory;
            _kennelFactory = kennelFactory;
            _animalFactory = animalFactory;
            _billFactory = billFactory;
            _customerFactory = customerFactory;

            _kennel = _kennelFactory.CreateKennel("Floofers", 19.99m);
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


        private IMenu CreateMenu(string title,
            IEnumerable<string> optionTags, string? returnKey = null,
            Action? returnAction = null)
        {
            int i = 0;
            _menuFactory.InitMenu(title);
            foreach (var option in optionTags)
                _menuFactory.AddMenuItem(++i, option, null);
            return _menuFactory.CreateMenu(returnKey, returnAction);
        }

        public void Init() { }

        public void Run()
        {
            while (true)
            {
                var selection = InputLoop(_mainMenu, "Select option: ");
                selection.Run?.Invoke();
            }
        }

        private void AddAnimal()
        {
            string? name = string.Empty;
            while (string.IsNullOrEmpty(name))
            {
                _output.Write("[Create new animal] (0 to return)", true);
                _output.Write("\nName: ", false);
                name = _input.Read();
                if (name == "0") return;
            }

            var selection = InputLoop(CustomerMenu, "Select owner: ");
            selection.Run?.Invoke();
            var owner = _kennel.GetCustomers().ElementAt(selection.Index - 1);

            _kennel.AddAnimal(_animalFactory.CreateAnimal(name, owner));
            _output.WriteLine($"{name} has been registered! Press any key to continue.", false);

            _input.ReadKey(true);
        }

        private void AddCustomer()
        {
            string? name = string.Empty;
            while (string.IsNullOrEmpty(name))
            {
                _output.Write("[Register customer] (0 to return)", true);
                _output.Write("\nName: ", false);
                name = _input.Read();
                if (name == "0") return;
            }

            _kennel.AddCustomer(_customerFactory.CreateCustomer(name));
            Pause($"{name} has been registered!");
        }

        private void Pause(string? message = null)
        {
            if (!string.IsNullOrEmpty(message))
                _output.WriteLine(message, false);

            _output.WriteLine("Press any key to continue.", false);
            _input.ReadKey(true);
        }

        private void ViewAnimals()
        {
            _output.WriteLine(AnimalMenu.ListMenu(), true);
            Pause();
        }

        private void ViewCustomers()
        {
            _output.WriteLine(CustomerMenu.ListMenu(), true);
            Pause();
        }

        private IMenuItem InputLoop(IMenu menu, string? prompt = null)
        {
            string? input;
            while (true)
            {
                _output.WriteLine(menu.SelectionMenu(), true);

                if (!string.IsNullOrEmpty(prompt))
                    _output.Write(prompt, false);

                input = _input.Read();
                if (!int.TryParse(input, out int choice)) continue;

                var selection = menu.MenuItems.Where(i => i.Index == choice).FirstOrDefault();
                if (selection == null) continue;

                return selection;
            }
        }

        private void ViewAnimalsAtKennel()
        {
            _output.WriteLine(CustomerMenu.ListMenu(), true);
            Pause();
        }

        private void CheckInAnimal()
        {
            var selection = InputLoop(AnimalMenu, "Select animal: ");
            var animal = _kennel.GetAnimals().ElementAt(selection.Index - 1);

            int i = 0;
            _menuFactory.InitMenu("Services");
            foreach (var service in _kennel.GetServices())
                _menuFactory.AddMenuItem(++i, $"{service.Name} - {service.Price}kr", null);
            var availableServices = _menuFactory.CreateMenu("None", null);

            List<IService> selectedServices = new();

            while (selection.Index != 0 && availableServices.MenuItems.Any())
            {
                _output.WriteLine(availableServices.SelectionMenu(), true);

                if (selectedServices.Any())
                {
                    string services = string.Join(", ", selectedServices.Select(s => s.Name));
                    _output.WriteLine($"Selected services: {services}", false);
                }

                selection = InputLoop(availableServices, "Select service: ");
                selectedServices.Add(_kennel.GetServices().ElementAt(selection.Index - 1));
                availableServices.MenuItems = availableServices.MenuItems
                    .Where(mi => mi.Index != selection.Index);
            }

            animal.IsAtKennel = true;
            Pause($"{animal.Name} has been checked in!");
        }

        private void CheckOutAnimal()
        {
            throw new NotImplementedException();
        }


        private void ListAnimalsAtKennel()
        {
        }
    }
}
