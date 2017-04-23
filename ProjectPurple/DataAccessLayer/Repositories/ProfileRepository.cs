using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data.Entity;

namespace DataAccessLayer.Repositories
{
    public class ProfileRepository:IProfileRepository, IDisposable
    {
        private HotelDataModelContainer _context;

        public ProfileRepository(HotelDataModelContainer context)
        {
            this._context = context;
        }

        public Address GetAddress(int id)
        {
            return _context.Addresses.Find(id);
        }

        public IEnumerable<Address> GetAddresses()
        {
            throw new NotImplementedException();
        }

        public Email GetEmail(int id)
        {
            return _context.Emails.Find(id);
        }

        public IEnumerable<Email> GetEmails()
        {
            throw new NotImplementedException();
        }

        public Guest GetGuest(Guid id)
        {
            return _context.Guests.Find(id);
        }

        public IEnumerable<Guest> GetGuests()
        {
            return _context.Guests.ToList();
        }

        public PhoneNumber GetPhoneNumber(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PhoneNumber> GetPhoneNumbers()
        {
            throw new NotImplementedException();
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

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ProfileRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
