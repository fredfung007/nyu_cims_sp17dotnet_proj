namespace DataAccessLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staffs
    {
        [Key]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        public string HashedPassword { get; set; }
    }
}
