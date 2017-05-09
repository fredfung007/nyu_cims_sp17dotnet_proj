using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.EF;
using HotelBookingWebsite.Models;

namespace HotelBookingWebsite.Helper
{
    public static class ModelExtensions
    {
        public static List<Guest> ToGuestList(this List<GuestViewModel> guestModels)
        {
            var order = 0;
            return (from t in guestModels
                where !string.IsNullOrEmpty(t.LastName) || !string.IsNullOrEmpty(t.FirstName)
                select new Guest
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Order = order++
                }).ToList();
        }

        public static List<GuestViewModel> ToGuestModelList(this List<Guest> guests)
        {
            var guestModelList = guests.Select(guest => new GuestViewModel
                {
                    FirstName = guest.FirstName,
                    LastName = guest.LastName,
                    Id = guest.Id,
                    Order = guest.Order
                })
                .ToList();

            return guestModelList.OrderBy(guest => guest.Order).ToList();
        }

        public static List<GuestViewModel> CleanEmpty(this List<GuestViewModel> guests)
        {
            guests.RemoveAll(guest => string.IsNullOrWhiteSpace(guest.FirstName) &&
                                            string.IsNullOrWhiteSpace(guest.LastName));
            return guests;
        }

        public static DateTime GetStartDateTime(this DateTime date)
        {
            return date.Date.Date.AddHours(12);
        }

        public static DateTime GetEndDateTime(this DateTime date)
        {
            return date.Date.Date.AddHours(14);
        }
    }
}