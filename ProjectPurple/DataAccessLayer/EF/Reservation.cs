namespace DataAccessLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reservation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservation()
        {
            DailyPrices = new HashSet<DailyPrice>();
            Guests = new HashSet<Guest>();
        }

        public Guid Id { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public bool isPaid { get; set; }

        public DateTime? checkInDate { get; set; }

        public DateTime? checkOutDate { get; set; }

        public Guid BillingInfo { get; set; }

        public Guid RoomType_Id { get; set; }

        [StringLength(30)]
        public string User_Username { get; set; }

        [StringLength(128)]
        public string AspNetUsers_Id { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DailyPrice> DailyPrices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Guest> Guests { get; set; }

        public virtual Profile Profile { get; set; }

        public virtual RoomType RoomType { get; set; }

        public virtual User User { get; set; }
    }
}
