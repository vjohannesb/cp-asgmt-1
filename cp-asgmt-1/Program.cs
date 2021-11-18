using Autofac;
using cp_asgmt_1.Interfaces;

namespace cp_asgmt_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = AFConfig.Configure();
            using var scope = container.BeginLifetimeScope();
            var app = scope.Resolve<IApplication>();
            app.Run();
        }
    }
}