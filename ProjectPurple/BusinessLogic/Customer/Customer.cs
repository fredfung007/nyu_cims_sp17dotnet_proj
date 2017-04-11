namespace BusinessLogic.Customer
{
    // Implementation of ICustomer, a customer who will stay at the hotel.
    class Customer : ICustomer
    {
        string first;
        string last;

        Customer(string firstName, string lastName)
        {
            first = firstName;
            last = lastName;
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
    }
}
