namespace CPAsgmt1.Interfaces.Kennel
{
    internal interface IService
    {
        public Guid ServiceId { get; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
