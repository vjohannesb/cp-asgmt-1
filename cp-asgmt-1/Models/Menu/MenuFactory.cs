using cp_asgmt_1.Interfaces.Menu;

namespace cp_asgmt_1.Models.Menu
{
    internal class MenuFactory : IMenuFactory
    {
        private IMenu _menu;
        private readonly Func<IMenu> _createMenu;
        private readonly Func<int, string, Action?, IMenuItem> _createMenuItem;

        public MenuFactory(IMenu menu, Func<IMenu> createMenu,
            Func<int, string, Action?, IMenuItem> createMenuItem)
        {
            _menu = menu;
            _createMenu = createMenu;
            _createMenuItem = createMenuItem;
        }

        public void InitMenu(string title)
        {
            _menu = _createMenu();
            _menu.Title = title;
        }

        public void AddMenuItem(int selector, string name, Action? run)
        {
            _menu.MenuItems = _menu.MenuItems.Append(_createMenuItem(selector, name, run));
        }

        public IMenu CreateMenu(string? returnKey, Action? returnAction)
        {
            if (!string.IsNullOrEmpty(returnKey))
                _menu.MenuItems = _menu.MenuItems.Append(_createMenuItem(0, returnKey, returnAction));
            return _menu;
        }
    }
}
