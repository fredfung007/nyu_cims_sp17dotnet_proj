using DataAccessLayer.Constants;

namespace DataAccessLayer.Constructor
{
    class AddressConstructor
    {
        private Address address;

        AddressConstructor()
        {
            address = new Address();
        }

        AddressConstructor addFirstLine(string firstLine)
        {
            address.FirstLine = firstLine;
            return this;
        }

        AddressConstructor addSecondLine(string secondLine)
        {
            address.SecondLine = secondLine;
            return this;
        }

        AddressConstructor addCity(string city)
        {
            address.City = city;
            return this;
        }

        AddressConstructor addState(US_STATE state)
        {
            address.State = state;
            return this;
        }

        AddressConstructor addZipcode(string zipcode)
        {
            address.ZipCode = zipcode;
            return this;
        }

        Address build()
        {
            return address;
        }
    }
}
