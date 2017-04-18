using System;
using System.Collections.Generic;
using DataAccessLayer;

namespace DataAccessLayer.Repositories
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

        [Obsolete]
        Guest getGuest(Guid Id);
        [Obsolete]
        IEnumerable<Guest> getGuests();
        [Obsolete]
        void InsertGuest(Guest guest);
        [Obsolete]
        void DeleteGuest(Guid Id);
        [Obsolete]
        void UpdateGuest(Guest guest);

        void save();
    }
}
