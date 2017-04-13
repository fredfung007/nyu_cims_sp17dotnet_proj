using System;
using System.Collections.Generic;
using DataAccessLayer;

namespace BusinessLogic.DAL
{
    // Interface for physical addresses.
    public interface IAddressRepository:IDisposable
    {
        Address getAddress(int Id);
        IEnumerable<Address> getAddresses();
        void save();
    }
}
