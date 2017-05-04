using System;

namespace DataAccessLayer.EF
{
    public class RoomOccupancy
    {
        public DateTime Date { get; set; }

        public Guid Id { get; set; }

        public int Occupancy { get; set; }

        public virtual RoomType RoomType { get; set; }
    }
}