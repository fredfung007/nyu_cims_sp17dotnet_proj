namespace DataAccessLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RoomTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoomTypes()
        {
            Reservations = new HashSet<Reservations>();
            RoomOccupancies = new HashSet<RoomOccupancies>();
        }

        public Guid Id { get; set; }

        public int BaseRate { get; set; }

        public int Inventory { get; set; }

        public int Type { get; set; }

        [Required]
        public string Ameneties { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservations> Reservations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomOccupancies> RoomOccupancies { get; set; }
    }
}
