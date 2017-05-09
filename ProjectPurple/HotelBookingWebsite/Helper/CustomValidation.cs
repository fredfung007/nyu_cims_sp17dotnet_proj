using HotelBookingWebsite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBookingWebsite.Helper
{
    public class MustHaveFirstGuestAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as List<GuestViewModel>;
            if (list == null)
            {
                return false;
            }

            var validCustomer = 0;

            foreach (var guest in list)
            {
                if (string.IsNullOrWhiteSpace(guest.FirstName) ^ string.IsNullOrWhiteSpace(guest.LastName))
                {
                    continue;
                }
                validCustomer++;
            }

            return validCustomer >= 1;
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