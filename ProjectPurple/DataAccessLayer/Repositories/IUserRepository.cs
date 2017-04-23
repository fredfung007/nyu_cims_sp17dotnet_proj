using System;
using System.Collections.Generic;
using DataAccessLayer.Constants;

namespace DataAccessLayer.Repositories
{
    public interface IUserRepository : IDisposable
    {
        User GetUser(string username);
        void InsertUser(User u);
        void DeleteUser(string username);
        void UpdateUser(User u);
        void Save();
    }
}
