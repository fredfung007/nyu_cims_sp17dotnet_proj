using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Constants;
using DataAccessLayer.EF;

namespace HotelBookingWebsite.Models
{
    public class ProfileViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Address First Line")]
        public string Address1 { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Address Second Line")]
        public string Address2 { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        public US_STATE State { get; set; }


        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Preferred Room Type")]
        public ROOM_TYPE PreferredRoomType { get; set; }

        public void SetByProfile(Profile profile)
        {
            FirstName = profile.FirstName;
            LastName = profile.LastName;
            Address1 = profile.Address.FirstLine;
            Address2 = profile.Address.SecondLine;
            City = profile.Address.City;
            State = profile.Address.State;
            PostalCode = profile.Address.ZipCode;
            PhoneNumber = profile.PhoneNumber;
        }
    }
}