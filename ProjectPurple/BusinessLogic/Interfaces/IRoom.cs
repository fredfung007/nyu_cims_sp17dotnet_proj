using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Constants;

namespace BusinessLogic.Interfaces
{
    interface IRoom
    {
        List<double> getRoomPriceList(roomType type, DateTime start, DateTime end);
        double getRoomPrice(roomType type, DateTime date);
        int getCurrentRoomAvailability(roomType type, DateTime date);
        bool setRoomInventory(roomType type, int amount);
        string getRoomDescription(roomType type);
        bool updateRoomDescription(roomType type, string description);
        string getRoomAmeneties(roomType type);
        List<string> getRoomPictureUrls(roomType type);
        bool updateRoomPictureUrls(roomType type, List<string> urls);
        bool insertPictureUrl(roomType type, string url);
    }
}
