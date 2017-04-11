using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Constants
{ 
    /// <summary>
    /// Enum type of room type
    /// </summary>
    public enum roomType
    {
        DOUBLE_BED_ROOM,
        QUEEN_ROOM,
        KING_BED_ROOM,
        SUITE
    };

    public enum cancelStatus
    {
        SUCCESS,
        FAILED_NON_EXIST,
        FAILED_ALREADY_CHECKED_IN,
        FAILED_EXPIRED
    };
}
