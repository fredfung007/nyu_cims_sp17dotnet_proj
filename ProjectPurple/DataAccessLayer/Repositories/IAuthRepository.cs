using System;
using System.Collections.Generic;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    // Interface for staff, which should also be a user.
    public interface IAuthRepository : IDisposable
    {
        //Staff GetStaff(string username);
        //IEnumerable<Staff> GetStaffs();
        //void InsertStaff(Staff staff);
        //void DeleteStaff(string username);
        //void UpdateStaff(Staff staff);

        AspNetUser GetUser(string username);
        IEnumerable<AspNetUser> GetUsers();
        void InsertUser(AspNetUser user);
        void DeleteUser(string username);
        void UpdateUser(AspNetUser user);
        int GetLoyaltyProgressByUserId(string username);

        void Save();
    }
}