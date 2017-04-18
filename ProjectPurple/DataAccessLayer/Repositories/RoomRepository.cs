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

        public void DeleteRoom(Guid confirmationNumber)
        {
            context.RoomTypes.Remove(context.RoomTypes.Find(confirmationNumber));
        }

        public void UpdateRoom(RoomType room)
        {
            context.Entry(room).State = System.Data.Entity.EntityState.Modified;
        }

        public RoomType getRoomType(Guid confirmationNumber)
        {
            return context.RoomTypes.Find(confirmationNumber);
        }

        public RoomType getRoomType(ROOM_TYPE type)
        {
            return context.RoomTypes.Where(room => room.Type == type).First();
        }

        public IEnumerable<RoomType> getRoomTypes()
        {
            return context.RoomTypes.AsEnumerable();
        }

        public void CheckIn(RoomType room, DateTime date)
        {
            UpdateRoomUsage(room, date, -1);
        }

        public void CheckOut(RoomType room, DateTime date)
        {
            UpdateRoomUsage(room, date, 1);
        }


        public void UpdateRoomUsage(RoomType room, DateTime date, int quantity)
        {
            // check if record exists
            RoomOccupancy roomOccupancy =
                context.RoomOccupancies.Where(ro => ro.Date == date && ro.RoomType == room).First();
            if (roomOccupancy != null)
            {
                // update the existing RoomOccupancy record
                roomOccupancy.Occupancy += quantity;
                UpdateRoomOccupancy(roomOccupancy);
            }
            else
            {
                // create new and add it into RoomOccupancies
                roomOccupancy = new RoomOccupancy {
                    Id = Guid.NewGuid(),
                    Date = date,
                    Occupancy = room.Inventory += quantity,
                    RoomType = room
                };
                context.RoomOccupancies.Add(roomOccupancy);
            }
        }

        public int GetRoomReservationAmount(RoomType room, DateTime date)
        {
            RoomOccupancy roomOccupancy =
                context.RoomOccupancies.Where(ro => ro.Date == date && ro.RoomType == room).First();
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

        void UpdateRoomInventory(RoomType room, int quantity)
        {
            room.Inventory = quantity;
            UpdateRoom(room);
        }

        void UpdateRoomOccupancy(RoomOccupancy roomOccupancy)
        {
            context.Entry(roomOccupancy).State = System.Data.Entity.EntityState.Modified;
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
