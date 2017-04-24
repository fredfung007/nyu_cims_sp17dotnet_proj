using System;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories
{
    // Interface for staff, which should also be a user.
    public interface IAuthRepository : IDisposable
    {
        Staff GetStaff(string username);
        IEnumerable<Staff> GetStaffs();
        void InsertStaff(Staff staff);
        void DeleteStaff(string username);
        void UpdateStaff(Staff staff);

        User GetUser(string username);
        IEnumerable<User> GetUsers();
        void InsertUser(User user);
        void DeleteUser(string username);
        void UpdateUser(User user);
        int GetLoyaltyProgressByUserId(string username);

        void Save();
    }
}
