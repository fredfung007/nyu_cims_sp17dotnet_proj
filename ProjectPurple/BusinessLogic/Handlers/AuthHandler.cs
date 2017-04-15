using DataAccessLayer.Repositories;
using DataAccessLayer;
using System.Web.Helpers;

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
        public bool verifyUserPassword(string username, string inputPassword)
        {
            User user = authRepository.getUser(username);
            return Crypto.VerifyHashedPassword(user.HashedPassword, inputPassword);
        }

        /// <summary>
        /// Verify the input password for given username versus the password in DB.
        /// </summary>
        /// <param name="username">username of a staff</param>
        /// <param name="inputPassword">input password</param>
        /// <returns></returns>
        public bool verifyStaffPassword(string username, string inputPassword)
        {
            Staff staff = authRepository.getStaff(username);
            return Crypto.VerifyHashedPassword(staff.HashedPassword, inputPassword);
        }
    }
}
