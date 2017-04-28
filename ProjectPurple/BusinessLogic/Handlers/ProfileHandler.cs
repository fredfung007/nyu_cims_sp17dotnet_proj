using System;
using DataAccessLayer;
using DataAccessLayer.EF;
using DataAccessLayer.Repositories;

namespace BusinessLogic.Handlers
{
    /// <summary>
    /// A handler class for maintaining profile information for user. maintain customer name, home address, room preference, loyalty program number, loyalty program status
    /// </summary>
    public class ProfileHandler
    {
        private readonly IProfileRepository _profileRepository;
        public ProfileHandler()
        {
            _profileRepository = new ProfileRepository(new CodeFirstHotelModel());
        }

        public ProfileHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        /// <summary>
        /// Get profile from profileId
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public Profile GetProfile(Guid profileId)
        {
            return _profileRepository.GetProfile(profileId);
        }

        /// <summary>
        /// Get address
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public Address GetAddress(Guid profileId)
        {
            return _profileRepository.GetProfile(profileId).Address;
        }

        /// <summary>
        /// Get email
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public string GetEmail(Guid profileId)
        {
            return _profileRepository.GetProfile(profileId).Email;
        }

        /// <summary>
        /// Get phone number
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public string GetPhoneNumber(Guid profileId)
        {
            return _profileRepository.GetProfile(profileId).PhoneNumber;
        }

        // get room preference
        // get loyalty program number

        /// <summary>
        /// set address for profile ID
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="address"></param>
        public void SetAddress(Guid profileId, Address address)
        {
            Profile profile = _profileRepository.GetProfile(profileId);
            profile.Address = address;
            _profileRepository.UpdateProfile(profile);
            _profileRepository.Save();
        }

        /// <summary>
        /// update email
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="email"></param>
        public void SetEmail(Guid profileId, string email)
        {
            Profile profile = _profileRepository.GetProfile(profileId);
            profile.Email = email;
            _profileRepository.UpdateProfile(profile);
            _profileRepository.Save();
        }

        /// <summary>
        /// update phonenumber
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="phoneNumber"></param>
        public void SetPhoneNumber(Guid profileId, string phoneNumber)
        {
            Profile profile = _profileRepository.GetProfile(profileId);
            profile.PhoneNumber = phoneNumber;
            _profileRepository.UpdateProfile(profile);
            _profileRepository.Save();
        }

        // TODO set room preference

        // TODO loyalty program number
       
    }
}
