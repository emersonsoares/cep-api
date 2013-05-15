using AddressApi.Base;

namespace AddressApi.Correios
{
    public class CorreiosRepository : IAddressRepository
    {
        public Address GetAddress(int zipCode)
        {
            return new Address(76873274, "Rua", "Juriti", "Setor 02", "Ariquemes", "Rondônia");
        }
    }
}
