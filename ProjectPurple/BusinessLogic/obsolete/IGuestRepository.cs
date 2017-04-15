using System;
using System.Collections.Generic;
using DataAccessLayer;

namespace BusinessLogic.obsolete
{
    public interface IGuestRepository:IDisposable
    {
        Guest getGuest(Guid Id);
        IEnumerable<Guest> getGuests();
        void save();
    }
}
