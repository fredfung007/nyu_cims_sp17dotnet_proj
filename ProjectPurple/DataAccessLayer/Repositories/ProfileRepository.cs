using System;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    public class ProfileRepository : IProfileRepository, IDisposable
    {
        private readonly HotelModelContext _context;

        public ProfileRepository(HotelModelContext context)
        {
            _context = context;
        }

        public Profile GetProfile(Guid id)
        {
            return _context.Profiles.Include(p => p.Address).FirstOrDefault(p => p.Id == id);
        }

        public void UpdateProfile(Profile profile)
        {
            _context.Entry(profile).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
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

        // ~ProfileRepository() {
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