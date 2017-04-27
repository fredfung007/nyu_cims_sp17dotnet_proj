namespace DataAccessLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Profiles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Profiles()
        {
            Reservations = new HashSet<Reservations>();
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

        public virtual Addresses Addresses { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual Emails Emails { get; set; }

        public virtual PhoneNumbers PhoneNumbers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
