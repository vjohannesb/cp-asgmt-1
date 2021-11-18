namespace CPAsgmt1.Interfaces.Kennel
{
    internal interface IKennelFactory
    {
        IKennel CreateKennel(string name, decimal price);

        IService CreateService(string name, decimal price);
    }
}