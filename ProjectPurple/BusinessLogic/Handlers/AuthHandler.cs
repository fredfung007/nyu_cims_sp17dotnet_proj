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

        public AuthHandler(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        /// <summary>
        /// Authorize username and plain text password for Staff.
        /// </summary>
        /// <param name="username">username of the Staff</param>
        /// <param name="inputpassword">plain text password</param>
        /// <returns></returns>
        public bool authorizeStaff(string username, string inputpassword)
        {
            Staff staff = authRepository.getStaff(username);
            return Crypto.VerifyHashedPassword(staff.HashedPassword, inputpassword);
        }

        /// <summary>
        /// Authorize username and plain text password for User.
        /// </summary>
        /// <param name="username">username of the User</param>
        /// <param name="inputpassword">plain text password</param>
        /// <returns></returns>
        public bool authorizeUser(string username, string inputpassword)
        {
            User user = authRepository.getUser(username);
            return Crypto.VerifyHashedPassword(user.HashedPassword, inputpassword);
        }

        void createAnonymousUser(string username, string inputpassword)
        {
            User user = createUser(username, inputpassword);
            user.isRegistered = false;
        }

        private User createUser(string username, string inputpassword)
        {
            User user = new User();
            user.Username = username;
            user.HashedPassword = Crypto.HashPassword(inputpassword);
            return user;
        }
    }
}
