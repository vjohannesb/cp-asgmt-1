using CPAsgmt1.Interfaces.Menu;

namespace CPAsgmt1.Models.Menu
{
    internal class Menu : IMenu
    {
        public Menu(string title, IEnumerable<IMenuItem> menuItems)
        {
            Title = title;
            MenuItems = menuItems;
        }

        public string Title { get; set; }
        public IEnumerable<IMenuItem> MenuItems { get; set; }
    }
}
