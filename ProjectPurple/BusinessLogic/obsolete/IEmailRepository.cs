using System;
using DataAccessLayer;
using System.Collections.Generic;

namespace BusinessLogic.DAL
{
    // Interface for email address.
    public interface IEmailRepository:IDisposable
    {
        // Return the Email Address.
        Email getEmail(int Id);
        IEnumerable<Email> getEmais();
        void save();
    }
}
