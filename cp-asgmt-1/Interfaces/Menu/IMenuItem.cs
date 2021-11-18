namespace cp_asgmt_1.Interfaces.Menu
{
    internal interface IMenuItem
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public Action Run { get; set; }
    }
}
