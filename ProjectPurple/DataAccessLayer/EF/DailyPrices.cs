namespace DataAccessLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DailyPrices
    {
        public Guid Id { get; set; }

        public int BillingPrice { get; set; }

        public DateTime Date { get; set; }

        public Guid Reservation_Id { get; set; }

        public virtual Reservations Reservations { get; set; }
    }
}
