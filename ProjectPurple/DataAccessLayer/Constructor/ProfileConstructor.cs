using System;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;

namespace DataAccessLayer.Constructor
{
    public class ProfileConstructor
    {
        private readonly Profile _profile;

        public ProfileConstructor(Guid id)
        {
            _profile = new Profile
            {
                Id = id
            };
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

        public ProfileConstructor AddPreferredRoomType(ROOM_TYPE roomType)
        {
            _profile.PreferredRoomType = roomType;
            return this;
        }

        public Profile Build()
        {
            return _profile;
        }
    }
}