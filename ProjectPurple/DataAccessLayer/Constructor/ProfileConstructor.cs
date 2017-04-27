using DataAccessLayer.EF;

namespace DataAccessLayer.Constructor
{
    public class ProfileConstructor
    {
        private readonly Profile _profile;

        public ProfileConstructor()
        {
            _profile = new Profile();
        }

        public ProfileConstructor AddName(string firstName, string lastName)
        {
            _profile.FirstName = firstName;
            _profile.LastName = lastName;
            return this;
        }

        public ProfileConstructor AddAddress(Address address)
        {
            _profile.Address = address;
            return this;
        }

        public ProfileConstructor AddEmail(string emailAddress)
        {
            _profile.Email = emailAddress;
            return this;
        }

        public ProfileConstructor AddPhoneNumber(string phoneNumber)
        {
            _profile.PhoneNumber = phoneNumber;
            return this;
        }

        public Profile Build()
        {
            return _profile;
        }
    }
}