using System;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Constants;

namespace DataAccessLayer.EF
{
    public class RoomType
    {
        public Guid Id { get; set; }

        public int BaseRate { get; set; }

        public int Inventory { get; set; }

        public ROOM_TYPE Type { get; set; }

        [Required]
        public string Ameneties { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}