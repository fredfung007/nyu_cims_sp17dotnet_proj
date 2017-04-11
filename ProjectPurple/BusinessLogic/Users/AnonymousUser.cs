namespace BusinessLogic.Users
{
    class AnonymousUser : IUser
    {
        public string getUserName()
        {
            return "Anonymous User";
        }
    }
}
