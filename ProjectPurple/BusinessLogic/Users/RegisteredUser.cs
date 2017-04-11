namespace BusinessLogic.Users
{
    class RegisteredUser : IUser
    {
        private string userName;

        RegisteredUser(string userName)
        {
            this.userName = userName;
        }
        public string getUserName()
        {
            return this.userName;
        }
    }
}
