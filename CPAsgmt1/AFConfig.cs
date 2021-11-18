using Autofac;
using CPAsgmt1.Interfaces;
using CPAsgmt1.Interfaces.Animals;
using CPAsgmt1.Interfaces.Billing;
using CPAsgmt1.Interfaces.Customers;
using CPAsgmt1.Interfaces.IO;
using CPAsgmt1.Interfaces.Kennel;
using CPAsgmt1.Interfaces.Menu;
using CPAsgmt1.Models.Animals;
using CPAsgmt1.Models.Billing;
using CPAsgmt1.Models.Customers;
using CPAsgmt1.Models.IO;
using CPAsgmt1.Models.Kennel;
using CPAsgmt1.Models.Menu;

namespace CPAsgmt1
{
    public static class AFConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            // Register application
            builder.RegisterType<Application>().As<IApplication>();

            // Register types
            builder.RegisterType<KennelFactory>().As<IKennelFactory>();
            builder.RegisterType<Kennel>().As<IKennel>();
            builder.RegisterType<Service>().As<IService>();

            builder.RegisterType<Customer>().As<ICustomer>();
            builder.RegisterType<Animal>().As<IAnimal>();
            builder.RegisterType<Bill>().As<IBill>();

            builder.RegisterType<MenuItem>().As<IMenuItem>();
            builder.RegisterType<ConsoleMenu>().As<IMenu>();
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<ConsoleInput>().As<IInput>();

            builder.RegisterType<MenuFactory>().As<IMenuFactory>();
            builder.RegisterType<BillFactory>().As<IBillFactory>();
            builder.RegisterType<AnimalFactory>().As<IAnimalFactory>();
            builder.RegisterType<CustomerFactory>().As<ICustomerFactory>();

            return builder.Build();
        }
    }
}
