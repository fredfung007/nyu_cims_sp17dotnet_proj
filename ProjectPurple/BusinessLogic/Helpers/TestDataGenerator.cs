using BusinessLogic.Handlers;
using DataAccessLayer.EF;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class TestDataGenerator
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private ReservationHandler _reservationHandler;

        public TestDataGenerator()
        {
            _reservationRepository = new ReservationRepository(new CodeFirstHotelModel());
            _roomRepository = new RoomRepository(new CodeFirstHotelModel());
            _reservationHandler = new ReservationHandler();
        }

        public void GenerateReservations(string username, int number)
        {
            List<Reservation> reservations = new List<Reservation>();
            for (int i = 0;i < number;i++)
            {
            }
        }
    }
}
