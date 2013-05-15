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
            var expected = new Address(76873274, "Rua", "Juriti", "Setor 02", "Ariquemes", "Rondônia");

            var repository = new CorreiosRepository();

            Assert.IsTrue(repository.GetAddress(76873274).Equals(expected));
        }
    }
}
