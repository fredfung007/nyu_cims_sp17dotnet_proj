using DataAccessLayer.Constants;

namespace BusinessLogic.Helpers
{
    // Interface for converters from string to state enum
    public interface IStateConverter
    {
        // Convert states to its full names.
        string GetState(UsState state);
        // Convert string to the state enum.
        UsState GetStateByName(string name);
    }
}
