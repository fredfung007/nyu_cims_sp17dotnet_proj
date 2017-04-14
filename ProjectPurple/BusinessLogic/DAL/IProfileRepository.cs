using System;
using System.Collections.Generic;
using DataAccessLayer;

namespace BusinessLogic.DAL
{
    /// <summary>
    /// Used to store billing information of one reservation.
    /// Can be different with information of the user
    /// who creates the reservation.
    /// </summary>
    public interface IProfileRepository:IDisposable
    {
        Profile getProfile(Guid Id);
        IEnumerable<Profile> getProfiles();
        void InsertProfile(Profile profile);
        void DeleteProfile(Guid Id);
        void UpdateProfile(Profile profile);

        Address getAddress(Guid Id);
        IEnumerable<Address> getAddresses();
        void InsertAddress(Address address);
        void DeleteAddress(Guid Id);
        void UpdateAddress(Address address);

        Email getEmail(Guid Id);
        IEnumerable<Email> getEmails();
        void InsertEmail(Email email);
        void DeleteEmail(Guid Id);
        void UpdateEmail(Email email);

        Guest getGuest(Guid Id);
        IEnumerable<Guest> getGuests();
        void InsertGuest(Guest guest);
        void DeleteGuest(Guid Id);
        void UpdateGuest(Guest guest);

        PhoneNumber getPhoneNumber(Guid Id);
        IEnumerable<PhoneNumber> getPhoneNumbers();
        void InsertPhoneNumber(PhoneNumber phoneNumber);
        void DeletePhoneNumber(Guid Id);
        void UpdatePhoneNumber(PhoneNumber phoneNumber);

        void save();
    }
}
