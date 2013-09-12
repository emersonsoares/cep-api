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
            var expected = new Address("76873274", "Rua", "Juriti", "Setor 02", "Ariquemes", "RO");

            var repository = new CorreiosRepository();

            var address = repository.GetAddress("76873274");

            Assert.IsInstanceOfType(address, typeof(Address));
            Assert.IsTrue(expected.Equals(address));
        }
    }
}
