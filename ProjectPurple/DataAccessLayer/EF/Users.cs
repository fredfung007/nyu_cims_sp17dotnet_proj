namespace DataAccessLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Reservations = new HashSet<Reservations>();
        }

        [Key]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        public string HashedPassword { get; set; }

        public bool isRegistered { get; set; }

        public string LoyalProgramNumber { get; set; }

        public int? LoyaltyProgress { get; set; }

        public DateTime? LoyaltyYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
