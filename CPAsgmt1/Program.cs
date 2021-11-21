using Autofac;
using CPAsgmt1.Interfaces;

namespace CPAsgmt1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IContainer? container = AFConfig.Configure();
            using ILifetimeScope? scope = container.BeginLifetimeScope();
            IApplication? app = scope.Resolve<IApplication>();
            app.Run();
        }
    }
}