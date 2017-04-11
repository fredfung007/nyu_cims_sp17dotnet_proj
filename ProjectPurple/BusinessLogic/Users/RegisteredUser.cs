namespace BusinessLogic.Users
{
    class RegisteredUser : IUser
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
