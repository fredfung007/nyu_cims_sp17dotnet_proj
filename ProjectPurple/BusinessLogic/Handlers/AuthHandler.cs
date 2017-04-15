using System;
using DataAccessLayer.Repositories;
using DataAccessLayer;

namespace BusinessLogic.Handlers
{
    public class AuthHandler
    {
        IAuthRepository authRepository;
        public AuthHandler()
        {
            authRepository = new AuthRepository(new HotelDataModelContainer());
        }

        /// <summary>
        /// Verify the input password for given username versus the password in DB.
        /// </summary>
        /// <param name="username">username of a user</param>
        /// <param name="inputPassword">input password</param>
        /// <returns></returns>
        public bool loginUser(string username, string inputPassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verify the input password for given username versus the password in DB.
        /// </summary>
        /// <param name="userName">username of a staff</param>
        /// <param name="inputPassword">input password</param>
        /// <returns></returns>
        public bool loginStaff(string userName, string inputPassword)
        {
            throw new NotImplementedException();
        }
    }
}
