using DataAccessLayer;

namespace BusinessLogic.Helpers
{
    // Interface for converters from string to state enum
    interface IStateConverter
    {
        // Convert states to its full names.
        string GetState(US_STATE state);
        // Convert string to the state enum.
        US_STATE GetStateByName(string name);
    }
}
