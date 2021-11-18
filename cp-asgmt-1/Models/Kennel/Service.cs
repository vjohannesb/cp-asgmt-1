using cp_asgmt_1.Interfaces.Kennel;

namespace cp_asgmt_1.Models.Kennel
{
    internal class Service : IService
    {
        public Service(string name, decimal price)
        {
            ServiceId = Guid.NewGuid();
            Name = name;
            Price = price;
        }

        public Guid ServiceId { get; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
