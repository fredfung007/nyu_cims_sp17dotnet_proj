using System;
using System.Data.Entity;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    internal class UserRepository : IUserRepository, IDisposable
    {
        private readonly HotelModelContext _context;

        public UserRepository(HotelModelContext context)
        {
            _context = context;
        }

        public void DeleteUser(string username)
        {
            AspNetUser user = _context.AspNetUsers.Find(username);
            if (user != null) _context.AspNetUsers.Remove(user);
        }

        public AspNetUser GetUser(string username)
        {
            return _context.AspNetUsers.Find(username);
        }

        public void InsertUser(AspNetUser user)
        {
            _context.AspNetUsers.Add(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateUser(AspNetUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        #region IDisposable Support

        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                    _context.Dispose();

                _disposedValue = true;
            }
        }

        // ~ReservationRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}