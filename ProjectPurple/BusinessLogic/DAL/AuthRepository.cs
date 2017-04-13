using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data.Entity;

namespace BusinessLogic.DAL
{
    class AuthRepository : IAuthRepository, IDisposable
    {
        private HotelDataModelContainer context;

        public AuthRepository(HotelDataModelContainer context)
        {
            this.context = context;
        }

        public Staff getStaff(Guid Id)
        {
            return context.Staffs.Find(Id);
        }

        public IEnumerable<Staff> getStaffs()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> getUers()
        {
            return context.Users.ToList();
        }

        public User getUser(Guid Id)
        {
            return context.Users.Find(Id);
        }

        public void InsertStaff(Staff staff)
        {
            context.Staffs.Add(staff);
        }

        public void DeleteStaff(int Id)
        {
            Staff staff = context.Staffs.Find(Id);
            context.Staffs.Remove(staff);
        }

        public void UpdateStaff(Staff staff)
        {
            throw new NotImplementedException();
        }

        public void InsertUser(User user)
        {
            context.Users.Add(user);
        }

        public void DeleteUser(int Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
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
