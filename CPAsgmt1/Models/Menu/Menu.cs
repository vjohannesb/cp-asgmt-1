using CPAsgmt1.Interfaces.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPAsgmt1.Models.Menu
{
    internal class Menu : IMenu
    {
        public string Title { get; set; }
        public IEnumerable<IMenuItem> MenuItems { get; set; }
    }
}
