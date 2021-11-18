using CPAsgmt1.Interfaces.Menu;

namespace CPAsgmt1.Models.Menu
{
    internal class MenuItem : IMenuItem
    {
        public MenuItem(int selector, string name, Action run)
        {
            Index = selector;
            Name = name;
            Run = run;
        }

        public int Index { get; set; }
        public string Name { get; set; }
        public Action Run { get; set; }
    }
}
