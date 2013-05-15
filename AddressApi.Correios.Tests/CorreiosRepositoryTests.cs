using AddressApi.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AddressApi.Correios.Tests
{
    [TestClass]
    public class CorreiosRepositoryTests
    {
        [TestMethod]
        public void CorreiosRepositoryGetAddressShouldBeReturnAValidAddress()
        {
            var expected = new Address
                               {
                                   City = "Ariquemes",
                                   Estate = "Rondônia",
                                   Neighborhood = "Setor 02",
                                   Street = "Juriti",
                                   TypeOfStreet = "Rua",
                                   ZipCode = 76873274
                               };

            var repository = new CorreiosRepository();

            Assert.IsTrue(repository.GetAddress(76873274).Equals(expected));
        }
    }
}
