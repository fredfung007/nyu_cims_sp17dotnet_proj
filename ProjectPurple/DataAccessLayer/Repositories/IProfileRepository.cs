using System;
using System.Collections.Generic;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    ///     Used to store billing information of one reservation.
    ///     Can be different with information of the user
    ///     who creates the reservation.
    /// </summary>
    public interface IProfileRepository : IDisposable
    {
        Profile GetProfile(Guid id);
        IEnumerable<Profile> GetProfiles();
        void InsertProfile(Profile profile);
        void DeleteProfile(Guid id);
        void UpdateProfile(Profile profile);

        [Obsolete]
        Guest GetGuest(Guid id);

        [Obsolete]
        IEnumerable<Guest> GetGuests();

        [Obsolete]
        void InsertGuest(Guest guest);

        [Obsolete]
        void DeleteGuest(Guid id);

        [Obsolete]
        void UpdateGuest(Guest guest);

        void Save();
    }
}