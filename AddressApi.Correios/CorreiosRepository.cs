using AddressApi.Base;

namespace AddressApi.Correios
{
    public class CorreiosRepository : IAddressRepository
    {
        public Address GetAddress(int zipCode)
        {
            var crawler = new CorreiosMobileCrawler(zipCode);
            return crawler.ParseDocument();
        }
    }
}
