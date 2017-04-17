using System;
using DataAccessLayer;
using System.Collections.Generic;

namespace BusinessLogic.obsolete
{
    // Interface for System User.
    interface IUserRepository:IDisposable
    {
        // Return the user name of the user
        User getUser(Guid Id);
        IEnumerable<User> getUsers();
        void save();
    }
}
