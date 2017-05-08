using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingWebsite.Models
{
    public class GlobalTimeViewModel
    {
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [DataType(DataType.Date)]
        public DateTime CurrentTime { get; set; }

        public bool Enabled { get; set; }
    }
}