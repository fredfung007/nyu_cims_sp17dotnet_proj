using DataAccessLayer.Constants;
using DataAccessLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Type;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingWebsite.Models
{
    public class SearchInputModel
    {
        [Required(ErrorMessage = "Please select a start date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please select an end date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }

    public class GuestViewModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public int Order { get; set; }
    }

    public class RetrieveModel
    {
        [Required(ErrorMessage = "Please input confirmation Id")]
        public string ConfirmationId { get; set;}
    }

    public class RoomSearchResultModel : TimeExpirationType
    {
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        public IList<RoomPriceDetail> RoomPriceDetails { get; set; }
        public int SelectedIndex { get; set; }
        public List<GuestViewModel> Guests { get; set; }
        public Guid ReservationId { get; set; }
        public bool IsConfirmed { get; set; }
        public string ConfirmationId { get; set; }
    }

    public class ConfirmationViewModel
    {
        // TODO merge with reservatonId
        public string ConfirmationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public string Ameneties { get; set; }
        public ICollection<Guest> Guests { get; set; }
        public Guid ReservationId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public List<int> PriceList { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsConfirmed { get; set; }
    }

    public class ResultViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public DateTime Expiration { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string SessionId { get; set; }
        public IList<RoomPriceDetail> RoomPriceDetails { get; set; }

        [Required(ErrorMessage = "Please select a room type")]
        public int SelectedIndex { get; set; }
    }

    public class InputGuestViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public DateTime Expiration { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string SessionId { get; set; }
        public List<GuestViewModel> Guests { get; set; }
    }

    public class CreateReservationViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public DateTime Expiration { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string SessionId { get; set; }

        public bool IsRoomAvailable { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<int> PriceList { get; set; }
        public string TypeName { get; set; }
    }

    //public class RoomSearchResult
    //{
    //    public Guid ResultId { get; set; }
    //    public IList<RoomPriceDetail> RoomPriceDetails { get; set; }
    //}

    public class RoomPriceDetail
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<int> PriceList { get; set; }
        public string Name { get; set; }
        public ROOM_TYPE Type { get; set; }
        public int AvaragePrice { get; set; }
        public string Description { get; set; }
        public string Ameneties { get; set; }
        public string PictureUlrs { get; set; }
    }

    // TODO
    //public class ReservationViewModel
    //{
    //    public Guid ConfirmationId { get; set; }
    //    public DateTime Start { get; set; }
    //    public DateTime End { get; set; }
    //    public DateTime CheckIn { get; set; }
    //    public DateTime CheckOut { get; set; }
    //    public List<Guest> Guests { get; set; }
    //    public string RoomUrl { get; set; }
    //    public List<int> PriceList { get; set; }
    //    public string Type { get; set; }
    //    public bool Cancel { get; set; }
    //    Profile BillInfo { get; set; }
    //}


}