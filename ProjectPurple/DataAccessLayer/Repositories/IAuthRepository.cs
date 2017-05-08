using System;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    // Interface for Getting Users From Database.
    public interface IAuthRepository : IDisposable
    {
        AspNetUser GetUser(string username);

        void UpdateUser(AspNetUser user);

        void Save();
    }
}