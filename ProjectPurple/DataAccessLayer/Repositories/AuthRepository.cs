using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DataAccessLayer.Repositories
{
    public class AuthRepository : IAuthRepository, IDisposable
    {
        private HotelDataModelContainer context;

        public AuthRepository(HotelDataModelContainer context)
        {
            this.context = context;
        }

        public Staff getStaff(string username)
        {
            return context.Staffs.Find(username);
        }

        public IEnumerable<Staff> getStaffs()
        {
            return context.Staffs.ToList();
        }

        public IEnumerable<User> getUsers()
        {
            return context.Users.ToList();
        }

        public User getUser(string username)
        {
            return context.Users.Find(username);
        }

        public void InsertStaff(Staff staff)
        {
            context.Staffs.Add(staff);
        }

        public void DeleteStaff(string username)
        {
            Staff staff = context.Staffs.Find(username);
            context.Staffs.Remove(staff);
        }

        public void UpdateStaff(Staff staff)
        {
            context.Entry(staff).State = EntityState.Modified;
        }

        public void InsertUser(User user)
        {
            context.Users.Add(user);
        }

        public void DeleteUser(string username)
        {
            User user = context.Users.Find(username);
            context.Users.Remove(user);
        }

        public void UpdateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

        public int GetLoyaltyProgressByUserId(string username)
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
        // ~AuthRepository() {
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
