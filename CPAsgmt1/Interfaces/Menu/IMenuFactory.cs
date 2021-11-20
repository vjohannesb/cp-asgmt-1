namespace CPAsgmt1.Interfaces.Menu
{
    internal interface IMenuFactory
    {
        void InitMenu(string title);
        void AddMenuItem(string name, Action? run);
        IMenu CreateMenu();
    }
}
