using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EF
{
    [Obsolete]
    public class Staff
    {
        [Key]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        public string HashedPassword { get; set; }
    }
}