using System.Data.Entity;
using DataAccessLayer;
using BusinessLogic.DAL;
using System;

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
            return profileRepository.getAddress(profileId);
        }

        Email GetEmail(Guid profileId)
        {
            return profileRepository.getEmail(profileId);
        }

        PhoneNumber GetPhoneNumber(Guid profileId)
        {
            return profileRepository.getPhoneNumber(profileId);
        }

        // get room preference
        // get loyalty program number

        void SetAddress(Guid profileId, Address address)
        {
            Profile profile = profileRepository.getProfile(profileId);
            profile.Addresse = address;
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
