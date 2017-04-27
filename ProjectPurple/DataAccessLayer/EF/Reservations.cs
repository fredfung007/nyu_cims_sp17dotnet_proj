namespace DataAccessLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reservations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservations()
        {
            DailyPrices = new HashSet<DailyPrices>();
            Guests = new HashSet<Guests>();
        }

        public Guid Id { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public bool isPaid { get; set; }

        public DateTime? checkInDate { get; set; }

        public DateTime? checkOutDate { get; set; }

        public Guid BillingInfo_Id { get; set; }

        public Guid RoomType_Id { get; set; }

        [StringLength(30)]
        public string User_Username { get; set; }

        [StringLength(128)]
        public string AspNetUsers_Id { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DailyPrices> DailyPrices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Guests> Guests { get; set; }

        public virtual Profiles Profiles { get; set; }

        public virtual RoomTypes RoomTypes { get; set; }

        public virtual Users Users { get; set; }
    }
}
