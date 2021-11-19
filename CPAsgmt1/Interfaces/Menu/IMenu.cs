namespace CPAsgmt1.Interfaces.Menu
{
    internal interface IMenu
    {
        public string Title { get; set; }
        public IEnumerable<IMenuItem> MenuItems { get; set; }
    }
}
