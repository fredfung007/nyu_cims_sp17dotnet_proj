using HotelBookingWebsite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBookingWebsite.Helper
{
    public class MustHaveFirsGuestAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as List<GuestViewModel>;
            if (list != null)
            {
                var firstGuest = list[0];
                if (firstGuest == null)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(firstGuest.FirstName) || string.IsNullOrEmpty(firstGuest.LastName))
                {
                    return false;
                }

                foreach (var guest in list)
                {
                    if (string.IsNullOrEmpty(guest.FirstName) ^ string.IsNullOrEmpty(guest.LastName))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

    public class MustHaveBothNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var guest = value as GuestViewModel;
            if (guest == null)
            {
                return true;
            }

            return !(string.IsNullOrEmpty(guest.FirstName) ^ string.IsNullOrEmpty(guest.LastName));
        }
    }
}