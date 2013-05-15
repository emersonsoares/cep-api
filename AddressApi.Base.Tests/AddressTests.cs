using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AddressApi.Base.Tests
{
    [TestClass]
    public class AddressTests
    {
        [TestMethod]
        public void AddressShouldBeAValueObject()
        {
            var address1 = new Address(76873274, "Rua", "Juriti", "Setor 02", "Ariquemes", "Rondônia");
            var address2 = new Address(76873274, "Rua", "Juriti", "Setor 02", "Ariquemes", "Rondônia");

            Assert.IsTrue(address1.Equals(address2));
        }
    }
}
