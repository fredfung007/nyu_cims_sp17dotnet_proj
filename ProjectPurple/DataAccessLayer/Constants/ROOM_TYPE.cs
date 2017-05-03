namespace DataAccessLayer.Constants
{
    /// <summary>
    ///     Enum type of room type
    /// </summary>
    public enum ROOM_TYPE
    {
        DoubleBedRoom = 0,
        QueenRoom = 1,
        KingBedRoom = 2,
        Suite = 3
    }

    public class NameString
    {
        readonly public static string[] ROOM_TYPE_NAME = new string[] { "Double Bed Room", "Queen Room", "King Room", "Suite" };
    }
}