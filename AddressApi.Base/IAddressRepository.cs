namespace AddressApi.Base
{
    public interface IAddressRepository
    {
        Address GetAddress(string zipCode);
    }
}
