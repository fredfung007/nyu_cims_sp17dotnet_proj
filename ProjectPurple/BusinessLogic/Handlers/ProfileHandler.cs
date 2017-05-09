using System;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using DataAccessLayer.Repositories;

namespace BusinessLogic.Handlers
{
    /// <summary>
    ///     A handler class for maintaining profile information for user. maintain customer name, home address, room
    ///     preference, loyalty program number, loyalty program status
    /// </summary>
    public class ProfileHandler
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileHandler()
        {
            _profileRepository = new ProfileRepository(new HotelModelContext());
        }

        public ProfileHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        /// <summary>
        ///     Get profile from profileId
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public Profile GetProfile(Guid profileId)
        {
            return _profileRepository.GetProfile(profileId);
        }

        /// <summary>
        ///     Get address
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public Address GetAddress(Guid profileId)
        {
            return _profileRepository.GetProfile(profileId).Address;
        }

        // get room preference
        // get loyalty program number

        /// <summary>
        ///     set address for profile ID
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="address"></param>
        public void SetAddress(Guid profileId, Address address)
        {
            Profile profile = _profileRepository.GetProfile(profileId);
            profile.Address = address;
            _profileRepository.UpdateProfile(profile);
        }

        /// <summary>
        ///     update phonenumber
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="phoneNumber"></param>
        public void SetPhoneNumber(Guid profileId, string phoneNumber)
        {
            Profile profile = _profileRepository.GetProfile(profileId);
            profile.PhoneNumber = phoneNumber;
            _profileRepository.UpdateProfile(profile);
        }

        public void SetRoomPreference(Guid profileId, ROOM_TYPE roomType)
        {
            Profile profile = _profileRepository.GetProfile(profileId);
            profile.PreferredRoomType = roomType;
            _profileRepository.UpdateProfile(profile);
        }

        public void SetName(Guid profileId, string firstName, string lastName)
        {
            Profile profile = _profileRepository.GetProfile(profileId);
            profile.FirstName = firstName;
            profile.LastName = lastName;
            _profileRepository.UpdateProfile(profile);
        }

        public void Save()
        {
            _profileRepository.Save();
        }
    }
}