using System;
using System.Linq;

namespace AddressApi.Base
{
    public class Address : IEquatable<Address>
    {
        public string TypeOfStreet { get; set; }

        public string Street { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string Estate { get; set; }

        public int ZipCode { get; set; }

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
