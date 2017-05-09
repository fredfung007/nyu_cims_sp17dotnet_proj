using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelModelContext _context;

        public RoomRepository(HotelModelContext context)
        {
            _context = context;
        }

        public void UpdateRoom(RoomType room)
        {
            _context.Entry(room).State = EntityState.Modified;
        }

        public RoomType GetRoomType(ROOM_TYPE type)
        {
            //throw new NotImplementedException();
            return _context.RoomTypes.FirstOrDefault(room => room.Type == type);
        }

        public IEnumerable<RoomType> GetRoomTypes()
        {
            return _context.RoomTypes.ToList();
        }

        public void UpdateRoomOccupancy(ROOM_TYPE type, DateTime date, int quantity)
        {
            // check if record exists
            RoomOccupancy roomOccupancy =
                _context.RoomOccupancies.FirstOrDefault(ro => ro.Date == date && ro.RoomType == type);
            if (roomOccupancy != null)
            {
                // update the existing RoomOccupancy record
                roomOccupancy.Occupancy += quantity;
                UpdateRoomOccupancy(roomOccupancy);
            }
            else
            {
                RoomType roomType = GetRoomType(type);
                // create new and add it into RoomOccupancy
                roomOccupancy = new RoomOccupancy
                {
                    Id = Guid.NewGuid(),
                    Date = date,
                    Occupancy = quantity,
                    RoomType = type
                };
                _context.RoomOccupancies.Add(roomOccupancy);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date">DateTime date is a date, no time</param>
        /// <returns></returns>
        public int GetRoomOccupancyByDate(ROOM_TYPE type, DateTime date)
        {
            RoomOccupancy roomOccupancy =
                _context.RoomOccupancies.FirstOrDefault(ro => ro.Date == date && ro.RoomType == type);

            return roomOccupancy?.Occupancy ?? 0;
        }

        public int GetRoomTotalAmount(ROOM_TYPE type)
        {
            return GetRoomType(type).Inventory;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateRoomOccupancy(RoomOccupancy roomOccupancy)
        {
            _context.Entry(roomOccupancy).State = EntityState.Modified;
        }

        public int GetMaxRoomOccupanciesByRoomTypeAfterDate(ROOM_TYPE type, DateTime date)
        {
            IEnumerable<RoomOccupancy> occupancies =
                _context.RoomOccupancies.Where(ro => ro.RoomType == type && ro.Date.CompareTo(date) >= 0);
            return occupancies.Select(occupancy => occupancy.Occupancy).Concat(new[] {0}).Max();
        }

        #region IDisposable Support

        private bool _disposedValue; // To detect redundant calls

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