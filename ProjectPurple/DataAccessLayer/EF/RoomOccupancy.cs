namespace DataAccessLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RoomOccupancy
    {
        public DateTime Date { get; set; }

        public Guid Id { get; set; }

        public int Occupancy { get; set; }

        public Guid RoomType_Id { get; set; }

        public virtual RoomType RoomType { get; set; }
    }
}
