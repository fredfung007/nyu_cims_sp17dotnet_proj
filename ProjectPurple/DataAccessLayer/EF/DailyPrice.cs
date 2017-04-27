using System;

namespace DataAccessLayer.EF
{
    public class DailyPrice
    {
        public Guid Id { get; set; }

        public int BillingPrice { get; set; }

        public DateTime Date { get; set; }

        public Guid ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}