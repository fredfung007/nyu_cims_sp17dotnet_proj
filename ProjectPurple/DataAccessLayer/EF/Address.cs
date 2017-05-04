using System;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Constants;

namespace DataAccessLayer.EF
{
    public class Address
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstLine { get; set; }

        public string SecondLine { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public US_STATE State { get; set; }

        [Required]
        public string ZipCode { get; set; }
    }
}