﻿using System;
using DataAccessLayer;
using System.Collections.Generic;

namespace BusinessLogic.DAL
{
    // Interface for Phone Number.
    interface IPhoneNumberRepository:IDisposable
    {
        // Return the phone number.
        PhoneNumber getPhoneNumber(Guid Id);
        IEnumerable<PhoneNumber> getPhoneNumbers();
        void save();
    }
}
