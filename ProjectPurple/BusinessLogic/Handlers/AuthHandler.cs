using DataAccessLayer.Repositories;
using DataAccessLayer;
using System.Web.Helpers;
using DataAccessLayer.EF;

namespace BusinessLogic.Handlers
{
    public class AuthHandler
    {
        private readonly IAuthRepository _authRepository;

        public AuthHandler()
        {
            _authRepository = new AuthRepository(new CodeFirstHotelModel());
        }

        public AuthHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        ///// <summary>
        ///// Authorize username and plain text password for Staff.
        ///// </summary>
        ///// <param name="username">username of the Staff</param>
        ///// <param name="inputpassword">plain text password</param>
        ///// <returns></returns>
        //public bool AuthorizeStaff(string username, string inputpassword)
        //{
            
        //    AspNetUser staff = _authRepository.GetStaff(username);
        //    return Crypto.VerifyHashedPassword(staff.HashedPassword, inputpassword);
        //}

        /// <summary>
        /// Authorize username and plain text password for User.
        /// </summary>
        /// <param name="username">username of the User</param>
        /// <param name="inputpassword">plain text password</param>
        /// <returns></returns>
        public bool AuthorizeUser(string username, string inputpassword)
        {
            AspNetUser user = _authRepository.GetUser(username);
            return Crypto.VerifyHashedPassword(user.PasswordHash, inputpassword);
        }

        private static AspNetUser CreateUser(string username, string inputpassword)
        {
            return new AspNetUser
            {
                UserName = username,
                PasswordHash = Crypto.HashPassword(inputpassword)
            };
        }
    }
}
