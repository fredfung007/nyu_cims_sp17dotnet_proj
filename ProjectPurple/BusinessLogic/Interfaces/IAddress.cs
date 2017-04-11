using DataAccessLayer;
using System;

namespace BusinessLogic.Address
{
    // Interface for physical addresses.
    public interface IAddress
    {
        String getAddress();
        String getAPTNumber();
        String getCity();
        US_STATE getStates();
        String getZipCode();
    }
}
