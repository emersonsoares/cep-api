namespace AddressApi.Base
{
    public interface IAddressRepository
    {
        Address GetAddress(int zipCode);
    }
}
