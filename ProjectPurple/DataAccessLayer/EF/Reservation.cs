using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DataAccessLayer.EF
{
    public class Reservation
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservation()
        {
            DailyPrices = new HashSet<DailyPrice>();
            Guests = new HashSet<Guest>();
        }

        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsPaid { get; set; }

        public DateTime? CheckInDate { get; set; }

        public DateTime? CheckOutDate { get; set; }

        public Guid BillingInfo { get; set; }

        public Guid RoomTypeId { get; set; }

        [StringLength(30)]
        public string UserUsername { get; set; }

        [StringLength(128)]
        public string AspNetUsersId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DailyPrice> DailyPrices { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Guest> Guests { get; set; }

        public virtual Profile Profile { get; set; }

        public virtual RoomType RoomType { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}