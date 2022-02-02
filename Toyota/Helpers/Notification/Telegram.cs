using System;
using System.Net;

namespace Toyota.Helpers.Notification
{
    public static class Telegram
    {
        private static String token = "5017905308:AAEXrEZvxJ1aWjc_GYVhVrQXlolOa-pVsxk";
        private static String chatId = "870452692";

        public static bool Send(String message)
        {
            String url = $"https://api.telegram.org/bot{token}/sendMessage?chat_id={chatId}&text={message}";
            try
            {
                var request = CreateRequest(url);
                var response = request.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static WebRequest CreateRequest(String url)
        {
            var request = WebRequest.Create(url);
            request.Method = "GET";
            return request;
        }
    }
}
