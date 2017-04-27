using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Constants;

namespace DataAccessLayer.Repositories
{
    public class RoomRepository : IRoomRepository, IDisposable
    {
        private readonly HotelDataModelContainer _context;

        public RoomRepository(HotelDataModelContainer context)
        {
            this._context = context;
        }

        public void InsertRoom(RoomType room)
        {
            _context.RoomTypes.Add(room);
        }

        public void DeleteRoom(Guid id)
        {
            RoomType room = _context.RoomTypes.Find(id);
            if (room != null) _context.RoomTypes.Remove(room);
        }

        public void UpdateRoom(RoomType room)
        {
            _context.Entry(room).State = System.Data.Entity.EntityState.Modified;
        }

        public RoomType GetRoomType(Guid id)
        {
            return _context.RoomTypes.Find(id);
        }

        public RoomType GetRoomType(ROOM_TYPE type)
        {
            return _context.RoomTypes.FirstOrDefault(room => room.Type == type);
        }

        public IEnumerable<RoomType> GetRoomTypes()
        {
            return _context.RoomTypes.ToList();
        }

        public void UpdateRoomUsage(RoomType room, DateTime date, int quantity)
        {
            // check if record exists
            RoomOccupancy roomOccupancy =
                _context.RoomOccupancies.FirstOrDefault(ro => ro.Date == date && ro.RoomType.Type == room.Type);
            if (roomOccupancy != null)
            {
                // update the existing RoomOccupancy record
                roomOccupancy.Occupancy += quantity;
                UpdateRoomOccupancy(roomOccupancy);
            }
            else
            {
                // create new and add it into RoomOccupancies
                roomOccupancy = new RoomOccupancy
                {
                    Id = Guid.NewGuid(),
                    Date = date,
                    Occupancy = room.Inventory + quantity,
                    RoomType = room
                };
                _context.RoomOccupancies.Add(roomOccupancy);
            }
        }

        public int GetRoomReservationAmount(RoomType room, DateTime date)
        {
            RoomOccupancy roomOccupancy =
                _context.RoomOccupancies.FirstOrDefault(ro => ro.Date == date && ro.RoomType.Type == room.Type);
            return roomOccupancy?.Occupancy ?? 0;
        }

        public int GetRoomTotalAmount(RoomType room)
        {
            return room.Inventory;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateRoomOccupancy(RoomOccupancy roomOccupancy)
        {
            _context.Entry(roomOccupancy).State = System.Data.Entity.EntityState.Modified;
        }

        /// <summary>
        /// Return all RoomOccupancies including and after the date
        /// </summary>
        /// <param name="type">ROOM_TYPE</param>
        /// <param name="date">date query starts</param>
        /// <returns></returns>
        public IEnumerable<RoomOccupancy> GetRoomOccupanciesByRoomTypeAfterDate(ROOM_TYPE type, DateTime date)
        {
            List<RoomOccupancy> roomOccupancies =
                _context.RoomOccupancies.Where(ro => ro.RoomType.Type == type && ro.Date.CompareTo(date) >= 0).ToList();
            return roomOccupancies;
        }

        public int GetMaxRoomOccupanciesByRoomTypeAfterDate(ROOM_TYPE type, DateTime date)
        {
            return _context.RoomOccupancies.Where(ro => ro.RoomType.Type == type && ro.Date.CompareTo(date) >= 0).Max(x => x.Occupancy);
        }


        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposedValue = true;
            }
        }

        // ~RoomRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
