namespace CPAsgmt1.Interfaces.Menu
{
    internal interface IMenuFactory
    {
        void InitMenu(string title);
        void AddMenuItem(int selector, string name, Action? run);
        IMenu CreateMenu(string? returnKey, Action? returnAction);
    }
}
