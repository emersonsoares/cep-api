using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AddressApi.Base.Tests
{
    [TestClass]
    public class AddressTests
    {
        [TestMethod]
        public void AddressShouldBeAValueObject()
        {
            var address1 = new Address
                               {
                                   ZipCode = 76873274,
                                   TypeOfStreet = "Rua",
                                   Street = "Juriti",
                                   Neighborhood = "Setor 02",
                                   City = "Ariquemes",
                                   Estate = "Rondônia"
                               };

            var address2 = new Address
                                {
                                    ZipCode = 76873274,
                                    TypeOfStreet = "Rua",
                                    Street = "Juriti",
                                    Neighborhood = "Setor 02",
                                    City = "Ariquemes",
                                    Estate = "Rondônia"
                                };

            Assert.IsTrue(address1.Equals(address2));
        }
    }
}
