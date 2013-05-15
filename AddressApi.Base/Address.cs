using System;
using System.Linq;

namespace AddressApi.Base
{
    public class Address : IEquatable<Address>
    {
        public Address(int zipCode, string typeOfStreet, string street, string neighborhood, string city, string estate)
        {
            ZipCode = zipCode;
            TypeOfStreet = typeOfStreet;
            Street = street;
            Neighborhood = neighborhood;
            City = city;
            Estate = estate;
        }

        public string TypeOfStreet { get; private set; }

        public string Street { get; private set; }

        public string Neighborhood { get; private set; }

        public string City { get; private set; }

        public string Estate { get; private set; }

        public int ZipCode { get; private set; }

        public bool Equals(Address other)
        {
            if (other == null) return false;

            if (ReferenceEquals(this, other)) return true;

            var properties = GetType().GetProperties();

            if (properties.Any())
            {
                return properties.All(property =>
                {
                    var left = property.GetValue(this, null);
                    var right = property.GetValue(other, null);

                    if (left is Address)
                        return ReferenceEquals(left, right);

                    return left.Equals(right);
                });
            }
            return true;
        }
    }
}
