using AddressApi.Base;

namespace AddressApi.Correios
{
    public class CorreiosRepository : IAddressRepository
    {
        public Address GetAddress(int zipCode)
        {
            return new Address
            {
                City = "Ariquemes",
                Estate = "Rondônia",
                Neighborhood = "Setor 02",
                Street = "Juriti",
                TypeOfStreet = "Rua",
                ZipCode = 76873274
            };
        }
    }
}
