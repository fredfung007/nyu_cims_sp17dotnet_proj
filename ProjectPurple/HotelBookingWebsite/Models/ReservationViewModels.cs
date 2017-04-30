using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBookingWebsite.Models
{
    public class AvailableRoomViewModel
    {
        public DateTime Expiration { get; set; }
        public string SessionId { get; set; }
        public IList<AvailableRoom> AvailableRooms { get; set; }
    }

    public class AvailableRoom
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public IList<int> PriceList { get; set; }
        public string Name { get; set; }
        public int AvaragePrice { get; set; }
        public string Description { get; set; }
        public string Ameneties { get; set; }
        public string PictureUlrs { get; set; }
    }
}