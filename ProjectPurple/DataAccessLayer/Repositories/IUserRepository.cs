using System;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    public interface IUserRepository : IDisposable
    {
        AspNetUser GetUser(string username);
        void InsertUser(AspNetUser u);
        void DeleteUser(string username);
        void UpdateUser(AspNetUser u);
        void Save();
    }
}