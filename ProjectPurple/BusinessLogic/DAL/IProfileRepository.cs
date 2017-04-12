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
        void save();
    }
}
