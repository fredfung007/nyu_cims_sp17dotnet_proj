namespace BusinessLogic.Customer
{
    /// <summary>
    /// Implementation of ICustomer, a customer who will stay at the hotel.
    /// </summary>
    class Customer : ICustomer
    {
        string first;
        string last;
        int id;

        Customer(string firstName, string lastName, int userId)
        {
            first = firstName;
            last = lastName;
            id = userId;
        }

        public string firstName
        {
            get
            {
                return first;
            }
        }

        public string lastName
        {
            get
            {
                return last;
            }
        }

        public int userId
        {
            get
            {
                return id;
            }
        }
    }
}
