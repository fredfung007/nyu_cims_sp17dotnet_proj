using System;
using System.Collections.Generic;
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

        public Guest GetGuest(Guid id)
        {
            return _context.Guests.Find(id);
        }

        public IEnumerable<Guest> GetGuests()
        {
            return _context.Guests.ToList();
        }

        public Profile GetProfile(Guid id)
        {
            return _context.Profiles.Find(id);
        }

        public IEnumerable<Profile> GetProfiles()
        {
            return _context.Profiles.ToList();
        }

        public void InsertProfile(Profile profile)
        {
            _context.Profiles.Add(profile);
        }

        public void DeleteProfile(Guid id)
        {
            Profile profile = _context.Profiles.Find(id);
            _context.Profiles.Remove(profile);
        }

        public void UpdateProfile(Profile profile)
        {
            _context.Entry(profile).State = EntityState.Modified;
        }

        public void InsertGuest(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void DeleteGuest(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateGuest(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Address GetAddress(int id)
        {
            return _context.Addresses.Find(id);
        }

        public IEnumerable<Address> GetAddresses()
        {
            throw new NotImplementedException();
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