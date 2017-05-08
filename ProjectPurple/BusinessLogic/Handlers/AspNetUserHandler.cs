using System;
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
        /// <param name="today">Date</param>
        /// <returns>Corresponding loyalty program progress</returns>
        public int FindLoyaltyProgramInfo(string username, DateTime today)
        {
            AspNetUser user = _userRepository.GetUser(username);
            if (user.LoyaltyYear == null || ((DateTime) user.LoyaltyYear).Year != today.Year)
            {
                var newYear = new DateTime(today.Year, 1, 1);
                user.LoyaltyProgress = 0;
                user.LoyaltyYear = newYear;
                _userRepository.UpdateUser(user);
                _userRepository.Save();
            }
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