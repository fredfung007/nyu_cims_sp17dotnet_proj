using System;

namespace DataAccessLayer.Repositories
{
    class UserRepository : IUserRepository, IDisposable
    {
        private HotelDataModelContainer _context;

        public UserRepository(HotelDataModelContainer context)
        {
            this._context = context;
        }

        public void DeleteUser(string username)
        {
            User user = _context.Users.Find(username);
            if (user != null) _context.Users.Remove(user);
        }

        public User GetUser(string username)
        {
            return _context.Users.Find(username);
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
        }
        
        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

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
