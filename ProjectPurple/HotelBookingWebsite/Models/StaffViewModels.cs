using DataAccessLayer.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingWebsite.Models
{
    public class CheckOutListModel
    {
        public Guid Id { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime checkInDate { get; set; }
        public DateTime checkOutDate { get; set; }
        public DateTime actualCheckInDate { get; set; }
    }

    public class CheckInListModel
    {
        public Guid Id { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime checkInDate { get; set; }
        public DateTime checkOutDate { get; set; }
    }

    public class InventoryModel
    {
        public ROOM_TYPE type { get; set; }
        public int inventory { get; set; }
    }

    public class OccupancyModel
    {
        public DateTime date { get; set; }
        public string rate { get; set; }
    }

    public class DashboardModel
    {
        public List<InventoryModel> inventory { get; set; }
        public List<CheckInListModel> checkInList { get; set; }
        public List<CheckOutListModel> checkOutList { get;  set;}
        public OccupancyModel occupancy { get; set; }
    }

    public class CheckInOutModel
    {
        public Guid confirmationNum { get; set; }
        public bool isSuccess { get; set; }
    }
}