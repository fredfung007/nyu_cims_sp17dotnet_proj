using System;
using DataAccessLayer.Constants;

namespace DataAccessLayer.EF
{
    public class RoomOccupancy
    {
        public DateTime Date { get; set; }

        public Guid Id { get; set; }

        public int Occupancy { get; set; }

        public ROOM_TYPE RoomType { get; set; }
    }
}