using System.Net;
using Newtonsoft.Json.Linq;

namespace BusinessLogic.Helpers
{
    public class RecaptchaHelper
    {
        public static bool GetRecaptchaResult(string response)
        {

            var secretKey = "6LeM_B8UAAAAAA9ROk7EWucqvEb6Hkql5aFQoGXJ";
            var client = new WebClient();
            var recap = client.DownloadString(string.Format(
                "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            JObject obj = JObject.Parse(recap);
            return (bool) obj.SelectToken("success");
        }
    }
}
