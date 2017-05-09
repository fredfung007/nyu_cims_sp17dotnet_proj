using System;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    ///     Used to store profile information for users.
    /// </summary>
    public interface IProfileRepository : IDisposable
    {
        Profile GetProfile(Guid id);
        void UpdateProfile(Profile profile);

        void Save();
    }
}