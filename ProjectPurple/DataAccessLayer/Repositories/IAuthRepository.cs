using System;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories
{
    // Interface for staff, which should also be a user.
    public interface IAuthRepository : IDisposable
    {
        Staff getStaff(string username);
        IEnumerable<Staff> getStaffs();
        void InsertStaff(Staff staff);
        void DeleteStaff(string username);
        void UpdateStaff(Staff staff);

        User getUser(string username);
        IEnumerable<User> getUsers();
        void InsertUser(User user);
        void DeleteUser(string username);
        void UpdateUser(User user);
        int GetLoyaltyProgressByUserId(string username);

        void save();
    }
}
