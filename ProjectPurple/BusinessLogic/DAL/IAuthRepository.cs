using System;
using DataAccessLayer;
using System.Collections.Generic;

namespace BusinessLogic.DAL       
{
    // Interface for staff, which should also be a user.
    interface IAuthRepository : IDisposable
    {
        Staff getStaff(Guid Id);
        IEnumerable<Staff> getStaffs();
        User getUser(Guid Id);
        IEnumerable<User> getUers();
        void save();
    }
}
