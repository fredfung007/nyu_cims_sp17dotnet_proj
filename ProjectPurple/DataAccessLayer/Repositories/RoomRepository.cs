using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
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
            throw new NotImplementedException();
        }

        public void DeleteRoom(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoom(RoomType room)
        {
            throw new NotImplementedException();
        }

        public RoomType getRoomType(Guid Id)
        {
            throw new NotImplementedException();
        }

        public RoomType getRoomType(ROOM_TYPE type)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomType> getRoomTypes()
        {
            throw new NotImplementedException();
        }

        public void UpdateRoomInventory(RoomType room, int quantity)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public int GetRoomReservationAmount(RoomType room, DateTime date)
        {
            throw new NotImplementedException();
        }

        public int GetRoomTotalAmount(RoomType room)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            context.SaveChanges();
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
