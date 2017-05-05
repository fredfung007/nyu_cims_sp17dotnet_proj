using System;
using System.Collections.Generic;
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

        //public Staff GetStaff(string username)
        //{
        //    return _context.Staffs.Find(username);
        //}

        //public IEnumerable<Staff> GetStaffs()
        //{
        //    return _context.Staffs.ToList();
        //}

        public IEnumerable<AspNetUser> GetUsers()
        {
            return _context.AspNetUsers.ToList();
        }

        public AspNetUser GetUser(string username)
        {
            return _context.AspNetUsers.Include(user => user.Profile).FirstOrDefault(user => user.UserName == username);
        }

        //public void InsertStaff(Staff staff)
        //{
        //    _context.Staffs.Add(staff);
        //}

        //public void DeleteStaff(string username)
        //{
        //    Staff staff = _context.Staffs.Find(username);
        //    if (staff != null) _context.Staffs.Remove(staff);
        //}

        //public void UpdateStaff(Staff staff)
        //{
        //    _context.Entry(staff).State = EntityState.Modified;
        //}

        public void InsertUser(AspNetUser user)
        {
            _context.AspNetUsers.Add(user);
        }

        public void DeleteUser(string username)
        {
            AspNetUser user = _context.AspNetUsers.Find(username);
            if (user != null) _context.AspNetUsers.Remove(user);
        }

        public void UpdateUser(AspNetUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public int GetLoyaltyProgressByUserId(string username)
        {
            throw new NotImplementedException();
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
                    _context.Dispose();

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