using System;
using DataAccessLayer;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories
{
    // Interface for staff, which should also be a user.
    public interface IAuthRepository : IDisposable
    {
        Staff getStaff(Guid Id);
        IEnumerable<Staff> getStaffs();
        void InsertStaff(Staff staff);
        void DeleteStaff(Guid Id);
        void UpdateStaff(Staff staff);

        User getUser(Guid Id);
        IEnumerable<User> getUsers();
        void InsertUser(User user);
        void DeleteUser(Guid Id);
        void UpdateUser(User user);
        int GetLoyaltyProgressByUserId(Guid Id);

        void save();
    }
}
