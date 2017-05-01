using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBookingWebsite.Models
{
    public class SearchInputModel
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
    public class ReservationRoomViewModel
    {
        public DateTime Expiration { get; set; }
        public string SessionId { get; set; }
        public IList<RoomSearchResult> RoomSearchResults { get; set; }
        public int SelectedIndex { get; set; }
        public List<Guest> Guests { get; set; }
        public Guid ReservationId { get; set; }
    }

    public class RoomSearchResult
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public IList<int> PriceList { get; set; }
        public string Name { get; set; }
        public ROOM_TYPE Type { get; set; }
        public int AvaragePrice { get; set; }
        public string Description { get; set; }
        public string Ameneties { get; set; }
        public string PictureUlrs { get; set; }
    }

    // TODO
    public class ReservationViewModel
    {
        public Guid ConfirmationId { get; set; }

    }
}