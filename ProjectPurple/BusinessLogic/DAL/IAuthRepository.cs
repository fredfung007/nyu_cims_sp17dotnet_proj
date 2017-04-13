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
        void InsertStaff(Staff staff);
        void DeleteStaff(int Id);
        void UpdateStaff(Staff staff);

        User getUser(Guid Id);
        IEnumerable<User> getUers();
        void InsertUser(User user);
        void DeleteUser(int Id);
        void UpdateUser(User user);

        void save();
    }
}
