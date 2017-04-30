using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBookingWebsite.Models
{
    public class AvailableRoomViewModel
    {
        public IList<AvailableRoom> AvailableRooms {get; set;}
    }

    public class AvailableRoom
    {
        public string Name { get; set; }
        public int AvaragePrice { get; set; }
        public string Description { get; set; }
        public string Ameneties { get; set; }
        public string PictureUlrs { get; set; }
    }
}