using DataAccessLayer.EF;
using HotelBookingWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBookingWebsite.Helper
{
    public static class ModelExtensions
    {
        public static List<Guest> ToGuestList(this List<GuestViewModel> guestModels)
        {
            List<Guest> guests = new List<Guest>();
            int order = 0;
            for (int i = 0; i < guestModels.Count; i++)
            {
                if (string.IsNullOrEmpty(guestModels[i].LastName) && string.IsNullOrEmpty(guestModels[i].FirstName))
                {
                    continue;
                }
                guests.Add(new Guest
                {
                    Id = guestModels[i].Id,
                    FirstName = guestModels[i].FirstName,
                    LastName = guestModels[i].LastName,
                    Order = order++,
                });
            }
            return guests;
        }

        public static List<GuestViewModel> ToGuestModelList(this List<Guest> guests)
        {
            List<GuestViewModel> guestModelList = new List<GuestViewModel>();
            foreach (Guest guest in guests)
            {
                guestModelList.Add(new GuestViewModel
                {
                    FirstName = guest.FirstName,
                    LastName = guest.LastName,
                    Id = guest.Id,
                    Order = guest.Order,
                });
            }
            guestModelList.OrderBy(guest => guest.Order);

            return guestModelList;
        }
    }
}