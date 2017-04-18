using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Constants;

namespace DataAccessLayer.Repositories
{
    public class RoomRepository : IRoomRepository, IDisposable
    {
        private HotelDataModelContainer context;

        public RoomRepository(HotelDataModelContainer context)
        {
            this.context = context;
        }

        public void InsertRoom(RoomType room)
        {
            context.RoomTypes.Add(room);
        }

        public void DeleteRoom(Guid Id)
        {
            context.RoomTypes.Remove(context.RoomTypes.Find(Id));
        }

        public void UpdateRoom(RoomType room)
        {
            context.Entry(room).State = System.Data.Entity.EntityState.Modified;
        }

        public RoomType getRoomType(Guid Id)
        {
            return context.RoomTypes.Find(Id);
        }

        public RoomType getRoomType(ROOM_TYPE type)
        {
            return context.RoomTypes.Where(room => room.Type == type).FirstOrDefault();
        }

        public IEnumerable<RoomType> getRoomTypes()
        {
            return context.RoomTypes.ToList();
        }

        public void UpdateRoomUsage(RoomType room, DateTime date, int quantity)
        {
            // check if record exists
            RoomOccupancy roomOccupancy =
                context.RoomOccupancies.Where(ro => ro.Date == date && ro.RoomType == room).FirstOrDefault();
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
                context.RoomOccupancies.Add(roomOccupancy);
            }
        }

        public int GetRoomReservationAmount(RoomType room, DateTime date)
        {
            RoomOccupancy roomOccupancy =
                context.RoomOccupancies.Where(ro => ro.Date == date && ro.RoomType == room).FirstOrDefault();
            return roomOccupancy != null ? roomOccupancy.Occupancy : 0;
        }

        public int GetRoomTotalAmount(RoomType room)
        {
            return room.Inventory;
        }

        public void save()
        {
            context.SaveChanges();
        }

        public void UpdateRoomOccupancy(RoomOccupancy roomOccupancy)
        {
            context.Entry(roomOccupancy).State = System.Data.Entity.EntityState.Modified;
        }

        /// <summary>
        /// Return all RoomOccupancies including and after the date
        /// </summary>
        /// <param name="type">ROOM_TYPE</param>
        /// <param name="date">date query starts</param>
        /// <returns></returns>
        public IEnumerable<RoomOccupancy> getRoomOccupanciesByRoomTypeAfterDate(ROOM_TYPE type, DateTime date)
        {
            List<RoomOccupancy> roomOccupancies =
                context.RoomOccupancies.Where(ro => ro.RoomType.Type == type && ro.Date.CompareTo(date) >= 0).ToList();
            return roomOccupancies;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RoomRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
