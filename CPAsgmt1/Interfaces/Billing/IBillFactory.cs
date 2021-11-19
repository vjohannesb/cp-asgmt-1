﻿using CPAsgmt1.Interfaces.Kennel;

namespace CPAsgmt1.Interfaces.Billing
{
    internal interface IBillFactory
    {
        public IBill CreateBill(IBillable recipient, decimal price, IEnumerable<IService>? services);
    }
}