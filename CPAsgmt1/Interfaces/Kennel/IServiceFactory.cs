using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPAsgmt1.Interfaces.Kennel
{
    internal interface IServiceFactory
    {
        IService CreateService(string name, decimal price);
    }
}
