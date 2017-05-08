using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.EF;
using DataAccessLayer.Constants;

namespace DataAccessLayer.Repositories
{
    public class RoomRepository : IRoomRepository, IDisposable
    {
        private readonly HotelModelContext _context;

        public RoomRepository(HotelModelContext context)
        {
            _context = context;
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
            _context.Entry(room).State = EntityState.Modified;
        }

        public RoomType GetRoomType(Guid id)
        {
            return _context.RoomTypes.Find(id);
        }

        public RoomType GetRoomType(Constants.ROOM_TYPE type)
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
                var roomType = GetRoomType(type);
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
        /// 
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

        public Task<RoomType> GetRoomTypeAsync(ROOM_TYPE id)
        {
            return _context.RoomTypes.FindAsync(id);
        }

        public void UpdateRoomOccupancy(RoomOccupancy roomOccupancy)
        {
            _context.Entry(roomOccupancy).State = EntityState.Modified;
        }

        /// <summary>
        ///     Return all RoomOccupancy including and after the date
        /// </summary>
        /// <param name="type">ROOM_TYPE</param>
        /// <param name="date">date query starts</param>
        /// <returns></returns>
        public IEnumerable<RoomOccupancy> GetRoomOccupanciesByRoomTypeAfterDate(Constants.ROOM_TYPE type, DateTime date)
        {
            var roomOccupancies =
                _context.RoomOccupancies.Where(ro => ro.RoomType == type && ro.Date.CompareTo(date) >= 0).ToList();
            return roomOccupancies;
        }

        public int GetMaxRoomOccupanciesByRoomTypeAfterDate(Constants.ROOM_TYPE type, DateTime date)
        {
            IEnumerable<RoomOccupancy> occupancies = _context.RoomOccupancies.Where(ro => ro.RoomType == type && ro.Date.CompareTo(date) >= 0);
            int maxOccupancy = 0;
            foreach (RoomOccupancy occupancy in occupancies)
            {
                maxOccupancy = maxOccupancy < occupancy.Occupancy ? occupancy.Occupancy : maxOccupancy;
            }
            return maxOccupancy;
        }

        #region IDisposable Support

        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                    _context.Dispose();

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
