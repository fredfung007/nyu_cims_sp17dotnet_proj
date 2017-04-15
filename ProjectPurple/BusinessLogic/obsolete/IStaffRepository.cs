using System;
using DataAccessLayer;
using System.Collections.Generic;

namespace BusinessLogic.obsolete       
{
    // Interface for staff, which should also be a user.
    interface IStaffRepository : IDisposable
    {
        Staff getStaff(Guid Id);
        IEnumerable<Staff> getStaffs();
        void save();
    }
}
