using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ReservationHandler
{
    interface IReservationHandler
    {
        void checkIn(Guid confirmationNumber);
        void checkOut(Guid confirmationNumber);
        void cancel(Guid confirmationNumber);
    }
}
