using CPAsgmt1.Interfaces.Menu;

namespace CPAsgmt1.Models.Menu
{
    internal class MenuFactory : IMenuFactory
    {
        private IMenu? _menu;
        private readonly Func<string, IEnumerable<IMenuItem>, IMenu> _createMenu;
        private readonly Func<string, Action?, IMenuItem> _createMenuItem;

        public MenuFactory(Func<string, IEnumerable<IMenuItem>, IMenu> createMenu,
            Func<string, Action?, IMenuItem> createMenuItem)
        {
            _createMenu = createMenu;
            _createMenuItem = createMenuItem;
        }

        public void InitMenu(string title)
        {
            _menu = _createMenu(title, new List<IMenuItem>());
        }

        public void AddMenuItem(string name, Action? run)
        {
            if (_menu == null) 
                throw new Exception("Must initialize menu before adding items.");

            _menu.MenuItems = _menu.MenuItems.Append(_createMenuItem(name, run));
        }

        public IMenu CreateMenu()
        {
            if (_menu == null)
                throw new Exception("Must initialize menu before creating it.");

            return _menu;
        }
    }
}
