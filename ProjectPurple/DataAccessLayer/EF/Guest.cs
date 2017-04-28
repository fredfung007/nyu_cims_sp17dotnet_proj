using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EF
{
    public class Guest
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public Guid ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}