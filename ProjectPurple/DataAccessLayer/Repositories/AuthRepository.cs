using System;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    public class AuthRepository : IAuthRepository, IDisposable
    {
        private readonly HotelModelContext _context;

        public AuthRepository(HotelModelContext context)
        {
            _context = context;
        }

        public AspNetUser GetUser(string username)
        {
            return _context.AspNetUsers.Include(user => user.Profile)
                .Include(user => user.Profile.Address)
                .FirstOrDefault(user => user.UserName == username);
        }

        #region IDisposable Support

        private bool _disposedValue; // To detect redundant calls

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

        // ~AuthRepository() {
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

        //public Staff GetStaff(string username)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Staff> GetStaffs()
        //{
        //    throw new NotImplementedException();
        //}

        //public void InsertStaff(Staff staff)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteStaff(string username)
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdateStaff(Staff staff)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}