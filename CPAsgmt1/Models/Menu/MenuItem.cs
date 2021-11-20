using CPAsgmt1.Interfaces.Menu;

namespace CPAsgmt1.Models.Menu
{
    internal class MenuItem : IMenuItem
    {
        public MenuItem(string name, Action run)
        {
            Name = name;
            Run = run;
        }

        public string Name { get; set; }
        public Action Run { get; set; }
    }
}
