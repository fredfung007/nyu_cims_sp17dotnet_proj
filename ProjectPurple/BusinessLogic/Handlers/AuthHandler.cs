using DataAccessLayer.Repositories;
using DataAccessLayer;
using System.Web.Helpers;

namespace BusinessLogic.Handlers
{
    public class AuthHandler
    {
        readonly IAuthRepository _authRepository;
        public AuthHandler()
        {
            _authRepository = new AuthRepository(new HotelDataModelContainer());
        }

        /// <summary>
        /// Authorize username and plain text password for Staff.
        /// </summary>
        /// <param name="username">username of the Staff</param>
        /// <param name="inputpassword">plain text password</param>
        /// <returns></returns>
        bool AuthorizeStaff(string username, string inputpassword)
        {
            Staff staff = _authRepository.GetStaff(username);
            return Crypto.VerifyHashedPassword(staff.HashedPassword, inputpassword);
        }

        /// <summary>
        /// Authorize username and plain text password for User.
        /// </summary>
        /// <param name="username">username of the User</param>
        /// <param name="inputpassword">plain text password</param>
        /// <returns></returns>
        bool AuthorizeUser(string username, string inputpassword)
        {
            User user = _authRepository.GetUser(username);
            return Crypto.VerifyHashedPassword(user.HashedPassword, inputpassword);
        }

        void CreateAnonymousUser(string username, string inputpassword)
        {
            User user = CreateUser(username, inputpassword);
            user.isRegistered = false;
        }

        private User CreateUser(string username, string inputpassword)
        {
            User user = new User();
            user.Username = username;
            user.HashedPassword = Crypto.HashPassword(inputpassword);
            return user;
        }
    }
}
