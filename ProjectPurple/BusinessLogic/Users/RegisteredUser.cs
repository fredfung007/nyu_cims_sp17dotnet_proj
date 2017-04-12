using DataAccessLayer;

namespace BusinessLogic.Users
{
    class RegisteredUser : User
    {
        private string username;

        public RegisteredUser(string username)
        {
            this.username = username;
        }
        public string getUsername()
        {
            return this.username;
        }
    }
}
