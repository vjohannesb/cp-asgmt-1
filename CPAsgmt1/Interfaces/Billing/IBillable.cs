namespace CPAsgmt1.Interfaces.Billing
{
    internal interface IBillable
    {
        public string IdNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
