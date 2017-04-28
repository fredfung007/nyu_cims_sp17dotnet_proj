using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DataAccessLayer.EF
{
    public class Profile
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Profile()
        {
            Reservations = new HashSet<Reservation>();
        }

        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int AddressId { get; set; }

        [StringLength(128)]
        public string IdAspNetUsersId { get; set; }

        public virtual Address Address { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}