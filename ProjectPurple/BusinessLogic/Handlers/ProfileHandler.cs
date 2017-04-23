using System.Data.Entity;
using System;
using DataAccessLayer;
using DataAccessLayer.Repositories;

namespace BusinessLogic.Handlers
{
    /// <summary>
    /// A handler class for maintaining profile information for user. maintain customer name, home address, room preference, loyalty program number, loyalty program status
    /// </summary>
    public class ProfileHandler
    {
        IProfileRepository profileRepository;
        public ProfileHandler()
        {
            profileRepository = new ProfileRepository(new HotelDataModelContainer());
        }

        public ProfileHandler(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        /// <summary>
        /// Get profile from profileId
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public Profile GetProfile(Guid profileId)
        {
            return profileRepository.getProfile(profileId);
        }

        /// <summary>
        /// Get address
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public Address GetAddress(Guid profileId)
        {
            return profileRepository.getProfile(profileId).Address;
        }

        /// <summary>
        /// Get email
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public Email GetEmail(Guid profileId)
        {
            return profileRepository.getProfile(profileId).Email;
        }

        /// <summary>
        /// Get phone number
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public PhoneNumber GetPhoneNumber(Guid profileId)
        {
            return profileRepository.getProfile(profileId).PhoneNumber;
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
            Profile profile = profileRepository.getProfile(profileId);
            profile.Address = address;
            profileRepository.UpdateProfile(profile);
            profileRepository.save();
        }

        /// <summary>
        /// update email
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="email"></param>
        public void SetEmail(Guid profileId, Email email)
        {
            Profile profile = profileRepository.getProfile(profileId);
            profile.Email = email;
            profileRepository.UpdateProfile(profile);
            profileRepository.save();
        }

        /// <summary>
        /// update phonenumber
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="phoneNumber"></param>
        public void SetPhoneNumber(Guid profileId, PhoneNumber phoneNumber)
        {
            Profile profile = profileRepository.getProfile(profileId);
            profile.PhoneNumber = phoneNumber;
            profileRepository.UpdateProfile(profile);
            profileRepository.save();
        }

        // TODOset room preference

        // set loyalty program number ?
       
    }
}
