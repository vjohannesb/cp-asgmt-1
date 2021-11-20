namespace CPAsgmt1.Interfaces.Menu
{
    internal interface IMenuItem
    {
        public string Name { get; set; }
        public Action Run { get; set; }
    }
}
