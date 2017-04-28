using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using DataAccessLayer.Constants;

namespace DataAccessLayer.EF
{
    public class RoomType
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoomType()
        {
            Reservations = new HashSet<Reservation>();
            RoomOccupancies = new HashSet<RoomOccupancy>();
        }

        public Guid Id { get; set; }

        public int BaseRate { get; set; }

        public int Inventory { get; set; }

        public ROOM_TYPE Type { get; set; }

        [Required]
        public string Ameneties { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomOccupancy> RoomOccupancies { get; set; }
    }
}