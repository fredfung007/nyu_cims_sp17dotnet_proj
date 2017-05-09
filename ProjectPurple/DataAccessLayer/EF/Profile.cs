using System;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Constants;

namespace DataAccessLayer.EF
{
    public class Profile
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public ROOM_TYPE PreferredRoomType { get; set; }

        public virtual Address Address { get; set; }
    }
}