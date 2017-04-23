﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DataAccessLayer.Repositories
{
    public class AuthRepository : IAuthRepository, IDisposable
    {
        private readonly HotelDataModelContainer _context;

        public AuthRepository(HotelDataModelContainer context)
        {
            _context = context;
        }

        public Staff GetStaff(string username)
        {
            return _context.Staffs.Find(username);
        }

        public IEnumerable<Staff> GetStaffs()
        {
            return _context.Staffs.ToList();
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(string username)
        {
            return _context.Users.Find(username);
        }

        public void InsertStaff(Staff staff)
        {
            _context.Staffs.Add(staff);
        }

        public void DeleteStaff(string username)
        {
            Staff staff = _context.Staffs.Find(username);
            if (staff != null) _context.Staffs.Remove(staff);
        }

        public void UpdateStaff(Staff staff)
        {
            _context.Entry(staff).State = EntityState.Modified;
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
        }

        public void DeleteUser(string username)
        {
            User user = _context.Users.Find(username);
            if (user != null) _context.Users.Remove(user);
        }

        public void UpdateUser(User user)
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

        #endregion
    }
}
