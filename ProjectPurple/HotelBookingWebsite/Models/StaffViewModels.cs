using DataAccessLayer.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingWebsite.Models
{
    public class CheckOutListModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        // TODO Why using this property?
        public DateTime ActualCheckInDate { get; set; }
    }

    public class CheckInListModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }

    public class InventoryModel
    {
        public ROOM_TYPE Type { get; set; }
        public int Inventory { get; set; }
        public string Rate { get; set; }
    }

    public class OccupancyModel
    {
        public DateTime Date { get; set; }
        public string Rate { get; set; }
    }

    public class DashboardModel
    {
        public List<InventoryModel> Inventory { get; set; }
        public List<CheckInListModel> CheckInList { get; set; }
        public List<CheckOutListModel> CheckOutList { get;  set;}
        public OccupancyModel Occupancy { get; set; }
    }

    public class CheckInOutModel
    {
        public Guid ConfirmationNum { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class OperationModel
    {
        public ConfirmationViewModel Confirmation { get; set; }
    }
}