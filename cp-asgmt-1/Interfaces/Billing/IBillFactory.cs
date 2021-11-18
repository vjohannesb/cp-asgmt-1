using cp_asgmt_1.Interfaces.Kennel;

namespace cp_asgmt_1.Interfaces.Billing
{
    internal interface IBillFactory
    {
        public IBill CreateBill(IBillable recipient, decimal price, IEnumerable<IService>? services);
    }
}
