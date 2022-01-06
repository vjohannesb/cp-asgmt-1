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
        private readonly IIO _io;
        private readonly IKennel _kennel;
        private readonly IMenu _mainMenu;

        public Application(IIO io, IMenuFactory menuFactory,
            IKennelFactory kennelFactory)
        {
            _io = io;
            _kennel = kennelFactory.CreateKennel("Floofers", 19.99m);
            _mainMenu = CreateMainMenu(menuFactory);

            Init();
        }

        public void Init()
        {
            _io.Init();
        }

        public void Run()
        {
            while (true)
            {
                IMenuItem? selection = _io.GetSelection(_mainMenu, "Enter selection: ", "Exit");
                if (selection == null) return;

                selection.Run?.Invoke();
            }
        }

        // Construct a main menu for the application
        private IMenu CreateMainMenu(IMenuFactory mf)
        {
            mf.InitMenu(_kennel.Name);
            mf.AddMenuItem("Register customer", _kennel.AddCustomer);
            mf.AddMenuItem("Register animal", _kennel.AddAnimal);
            mf.AddMenuItem("View customers", _kennel.ViewCustomers);
            mf.AddMenuItem("View animals", _kennel.ViewAnimals);
            mf.AddMenuItem("View animals at kennel", _kennel.ViewAnimalsAtKennel);
            mf.AddMenuItem("Check in animal", _kennel.CheckInAnimal);
            mf.AddMenuItem("Check out animal", _kennel.CheckOutAnimal);
            return mf.CreateMenu();
        }
    }
}
