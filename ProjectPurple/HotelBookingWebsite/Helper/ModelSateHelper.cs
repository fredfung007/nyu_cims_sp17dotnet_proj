//using HotelBookingWebsite.Models;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.ModelBinding;

//namespace HotelBookingWebsite.Helper
//{
//    public static class ModelStateHelpers
//    {
//        public static string SerialiseModelState(ModelStateDictionary modelState)
//        {
//            var errorList = modelState
//                .Select(kvp => new RoomSearchResultModel
//                {
//                    Key = kvp.Key,
//                    AttemptedValue = kvp.Value.AttemptedValue,
//                    RawValue = kvp.Value.RawValue,
//                    ErrorMessages = kvp.Value.Errors.Select(err => err.ErrorMessage).ToList(),
//                });

//            return JsonConvert.SerializeObject(errorList);
//        }

//        public static ModelStateDictionary DeserialiseModelState(string serialisedErrorList)
//        {
//            var errorList = JsonConvert.DeserializeObject<List<RoomSearchResultModel>>(serialisedErrorList);
//            var modelState = new ModelStateDictionary();

//            foreach (var item in errorList)
//            {
//                modelState.SetModelValue(item.Key, item.RawValue, item.AttemptedValue);
//                foreach (var error in item.ErrorMessages)
//                {
//                    modelState.AddModelError(item.Key, error);
//                }
//            }
//            return modelState;
//        }
//    }
//}