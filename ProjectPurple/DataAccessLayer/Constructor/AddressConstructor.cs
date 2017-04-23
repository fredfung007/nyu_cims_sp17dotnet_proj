using DataAccessLayer.Constants;

namespace DataAccessLayer.Constructor
{
    class AddressConstructor
    {
        private Address _address;

        AddressConstructor()
        {
            _address = new Address();
        }

        AddressConstructor AddFirstLine(string firstLine)
        {
            _address.FirstLine = firstLine;
            return this;
        }

        AddressConstructor AddSecondLine(string secondLine)
        {
            _address.SecondLine = secondLine;
            return this;
        }

        AddressConstructor AddCity(string city)
        {
            _address.City = city;
            return this;
        }

        AddressConstructor AddState(UsState state)
        {
            _address.State = state;
            return this;
        }

        AddressConstructor AddZipcode(string zipcode)
        {
            _address.ZipCode = zipcode;
            return this;
        }

        Address Build()
        {
            return _address;
        }
    }
}
