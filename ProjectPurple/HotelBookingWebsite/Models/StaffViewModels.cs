using DataAccessLayer.Constants;
using System;

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

    public class OccupanceyModel
    {
        public ROOM_TYPE type { get; set; }
        public int inventory { get; set; }
        public int occupancey { get; set; }
    }