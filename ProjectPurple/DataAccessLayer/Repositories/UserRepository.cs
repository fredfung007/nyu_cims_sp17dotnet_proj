using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    class UserRepository : IUserRepository, IDisposable
    {
        private HotelDataModelContainer context;

        public UserRepository(HotelDataModelContainer context)
        {
            this.context = context;
        }

        public void DeleteUser(string username)
        {
            context.Users.Remove(context.Users.Find(username));
        }

        public User GetUser(string username)
        {
            return context.Users.Find(username);
        }

        public void InsertUser(User user)
        {
            context.Users.Add(user);
        }

        public void save()
        {
            context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            context.Entry(user).State = System.Data.Entity.EntityState.Modified;
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
        // ~ReservationRepository() {
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
