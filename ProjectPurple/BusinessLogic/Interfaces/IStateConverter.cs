namespace BusinessLogic.Address
{
    // Interface for converters from string to state enum
    interface IStateConverter
    {
        // Convert states to its full names.
        string GetState(State state);
        // Convert string to the state enum.
        State GetStateByName(string name);
    }
}
