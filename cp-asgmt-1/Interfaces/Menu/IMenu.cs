namespace cp_asgmt_1.Interfaces.Menu
{
    internal interface IMenu
    {
        public string Title { get; set; }
        public IEnumerable<IMenuItem> MenuItems { get; set; }

        public string SelectionMenu();
        public string ListMenu();
    }
}
