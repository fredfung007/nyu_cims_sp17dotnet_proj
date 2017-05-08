using DataAccessLayer.EF;
using DataAccessLayer.Repositories;

namespace BusinessLogic.Handlers
{
    public class AspNetUserHandler
    {
        private readonly IAuthRepository _userRepository;

        public AspNetUserHandler()
        {
            _userRepository = new AuthRepository(new HotelModelContext());
        }

        public AspNetUser GetAspNetUser(string username)
        {
            return _userRepository.GetUser(username);
        }

        /// <summary>
        ///     Get the loyalty program progress by username.
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Corresponding loyalty program progress</returns>
        public int FindLoyaltyProgramInfo(string username)
        {
            AspNetUser user = _userRepository.GetUser(username);
            return user.LoyaltyProgress;
        }

        /// <summary>
        ///     Find the Profile of the given user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>The Profile of the given user.</returns>
        public Profile GetProfile(string username)
        {
            AspNetUser user = _userRepository.GetUser(username);
            return user.Profile;
        }
    }
}