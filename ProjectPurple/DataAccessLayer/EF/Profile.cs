namespace DataAccessLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Profile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Profile()
        {
            Reservations = new HashSet<Reservation>();
        }

        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public Guid PhoneNumber_Id { get; set; }

        public int Email_Id { get; set; }

        public int Address_Id { get; set; }

        [StringLength(128)]
        public string IdAspNetUsers_Id { get; set; }

        public virtual Address Address { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual Email Email { get; set; }

        public virtual PhoneNumber PhoneNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
