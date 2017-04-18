namespace DataAccessLayer.Constructor
{
    class ProfileConstructor
    {
        private Profile profile;

        ProfileConstructor()
        {
            profile = new Profile();
        }

        ProfileConstructor addName(string firstName, string lastName)
        {
            profile.FirstName = firstName;
            profile.LastName = lastName;
            return this;
        }

        ProfileConstructor addAddress(Address address)
        {
            profile.Address = address;
            return this;
        }

        ProfileConstructor addEmail(string emailAddress)
        {
            Email email= new Email();
            email.Address = emailAddress;
            profile.Email = email;
            return this;
        }

        ProfileConstructor addPhoneNumber(string phoneNumber)
        {
            PhoneNumber number = new PhoneNumber();
            number.Number = phoneNumber;
            profile.PhoneNumber = number;
            return this;
        }

        Profile build()
        {
            return profile;
        }
    }
}
