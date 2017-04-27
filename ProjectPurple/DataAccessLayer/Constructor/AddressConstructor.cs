using DataAccessLayer.Constants;
using DataAccessLayer.EF;

namespace DataAccessLayer.Constructor
{
    public class AddressConstructor
    {
        private readonly Address _address;

        public AddressConstructor()
        {
            _address = new Address();
        }

        public AddressConstructor AddFirstLine(string firstLine)
        {
            _address.FirstLine = firstLine;
            return this;
        }

        public AddressConstructor AddSecondLine(string secondLine)
        {
            _address.SecondLine = secondLine;
            return this;
        }

        public AddressConstructor AddCity(string city)
        {
            _address.City = city;
            return this;
        }

        public AddressConstructor AddState(US_STATE state)
        {
            _address.State = state;
            return this;
        }

        public AddressConstructor AddZipcode(string zipcode)
        {
            _address.ZipCode = zipcode;
            return this;
        }

        public Address Build()
        {
            return _address;
        }
    }
}
