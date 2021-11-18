using Autofac;
using cp_asgmt_1.Interfaces;
using cp_asgmt_1.Interfaces.Animals;
using cp_asgmt_1.Interfaces.Billing;
using cp_asgmt_1.Interfaces.Customers;
using cp_asgmt_1.Interfaces.IO;
using cp_asgmt_1.Interfaces.Kennel;
using cp_asgmt_1.Interfaces.Menu;
using cp_asgmt_1.Models.Animals;
using cp_asgmt_1.Models.Billing;
using cp_asgmt_1.Models.Customers;
using cp_asgmt_1.Models.IO;
using cp_asgmt_1.Models.Kennel;
using cp_asgmt_1.Models.Menu;

namespace cp_asgmt_1
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
