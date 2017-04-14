using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data.Entity;

namespace BusinessLogic.DAL
{
    class ProfileRepository:IProfileRepository, IDisposable
    {
        private HotelDataModelContainer context;

        public ProfileRepository(HotelDataModelContainer context)
        {
            this.context = context;
        }

        public Address getAddress(int Id)
        {
            return context.Addresses.Find(Id);
        }

        public IEnumerable<Address> getAddresses()
        {
            throw new NotImplementedException();
        }

        public Email getEmail(int Id)
        {
            return context.Emails.Find(Id);
        }

        public IEnumerable<Email> getEmails()
        {
            throw new NotImplementedException();
        }

        public Guest getGuest(Guid Id)
        {
            return context.Guests.Find(Id);
        }

        public IEnumerable<Guest> getGuests()
        {
            return context.Guests.ToList();
        }

        public PhoneNumber getPhoneNumber(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PhoneNumber> getPhoneNumbers()
        {
            throw new NotImplementedException();
        }

        public Profile getProfile(Guid Id)
        {
            return context.Profiles.Find(Id);
        }

        public IEnumerable<Profile> getProfiles()
        {
            return context.Profiles.ToList();
        }


        public void InsertProfile(Profile profile)
        {
            context.Profiles.Add(profile);
        }

        public void DeleteProfile(Guid Id)
        {
            Profile profile = context.Profiles.Find(Id);
            context.Profiles.Remove(profile);
        }

        public void UpdateProfile(Profile profile)
        {
            context.Entry(profile).State = EntityState.Modified;
        }

        public void InsertAddress(Address address)
        {
            throw new NotImplementedException();
        }

        public void DeleteAddress(int Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAddress(Address address)
        {
            throw new NotImplementedException();
        }

        public void InsertEmail(Email email)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmail(int Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmail(Email email)
        {
            throw new NotImplementedException();
        }

        public void InsertGuest(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void DeleteGuest(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateGuest(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void InsertPhoneNumber(PhoneNumber phoneNumber)
        {
            throw new NotImplementedException();
        }

        public void DeletePhoneNumber(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
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
