namespace DataAccessLayer.Constructor
{
    class ProfileConstructor
    {
        private Profile _profile;

        ProfileConstructor()
        {
            _profile = new Profile();
        }

        ProfileConstructor AddName(string firstName, string lastName)
        {
            _profile.FirstName = firstName;
            _profile.LastName = lastName;
            return this;
        }

        ProfileConstructor AddAddress(Address address)
        {
            _profile.Address = address;
            return this;
        }

        ProfileConstructor AddEmail(string emailAddress)
        {
            Email email= new Email();
            email.Address = emailAddress;
            _profile.Email = email;
            return this;
        }

        ProfileConstructor AddPhoneNumber(string phoneNumber)
        {
            PhoneNumber number = new PhoneNumber();
            number.Number = phoneNumber;
            _profile.PhoneNumber = number;
            return this;
        }

        Profile Build()
        {
            return _profile;
        }
    }
}
