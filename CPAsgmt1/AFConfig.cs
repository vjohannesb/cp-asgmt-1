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
            ContainerBuilder? builder = new();

            // Register application
            builder.RegisterType<Application>().As<IApplication>();

            // Register types
            builder.RegisterType<Kennel>().As<IKennel>();
            builder.RegisterType<Service>().As<IService>();

            builder.RegisterType<Bill>().As<IBill>();
            builder.RegisterType<Animal>().As<IAnimal>();
            builder.RegisterType<Customer>().As<ICustomer>();

            builder.RegisterType<Menu>().As<IMenu>();
            builder.RegisterType<MenuItem>().As<IMenuItem>();

            builder.RegisterType<ConsoleIO>().As<IIO>();

            builder.RegisterType<MenuFactory>().As<IMenuFactory>();
            builder.RegisterType<BillFactory>().As<IBillFactory>();
            builder.RegisterType<AnimalFactory>().As<IAnimalFactory>();
            builder.RegisterType<KennelFactory>().As<IKennelFactory>();
            builder.RegisterType<ServiceFactory>().As<IServiceFactory>();
            builder.RegisterType<CustomerFactory>().As<ICustomerFactory>();

            return builder.Build();
        }
    }
}
