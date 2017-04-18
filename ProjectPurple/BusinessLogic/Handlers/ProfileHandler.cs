using System.Data.Entity;
using System;
using DataAccessLayer;
using DataAccessLayer.Repositories;

namespace BusinessLogic.Handlers
{
    /// <summary>
    /// A handler class for maintaining profile information for user
    /// </summary>
    // maintain customer name, home address, room preference, loyalty program number
    // loyalty program status
    class ProfileHandler
    {
        IProfileRepository profileRepository;
        public ProfileHandler()
        {
            profileRepository = new ProfileRepository(new HotelDataModelContainer());
        }

        Profile GetProfile(Guid profileId)
        {
            return profileRepository.getProfile(profileId);
        }

        //TODO? use ICustomer or userId
        Address GetAddress(Guid profileId)
        {
            return profileRepository.getProfile(profileId).Address;
        }

        Email GetEmail(Guid profileId)
        {
            return profileRepository.getProfile(profileId).Email;
        }

        PhoneNumber GetPhoneNumber(Guid profileId)
        {
            return profileRepository.getProfile(profileId).PhoneNumber;
        }

        // get room preference
        // get loyalty program number

        void SetAddress(Guid profileId, Address address)
        {
            Profile profile = profileRepository.getProfile(profileId);
            profile.Address = address;
            profileRepository.UpdateProfile(profile);
            profileRepository.save();
        }

        void SetEmail(Guid profileId, Email email)
        { 
        }

        void SetPhoneNumber(Guid profileId, PhoneNumber phoneNumber)
        {
        }

        // set room preference

        // set loyalty program number ?
        
        // TODO: update loyalty program status
       
    }
}
