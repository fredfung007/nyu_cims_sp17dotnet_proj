using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DataAccessLayer.EF
{
    [Obsolete]
    public class User
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Reservations = new HashSet<Reservation>();
        }

        [Key]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        public string HashedPassword { get; set; }

        public bool IsRegistered { get; set; }

        public string LoyalProgramNumber { get; set; }

        public int? LoyaltyProgress { get; set; }

        public DateTime? LoyaltyYear { get; set; }

        public Profile Profile { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}