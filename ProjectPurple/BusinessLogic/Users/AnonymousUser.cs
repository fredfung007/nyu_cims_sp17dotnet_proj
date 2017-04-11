using System;

namespace BusinessLogic.Users
{
    class AnonymousUser : RegisteredUser
    {
        public AnonymousUser() : base ("anonymous_" + Guid.NewGuid())
        {
        }
    }
}
