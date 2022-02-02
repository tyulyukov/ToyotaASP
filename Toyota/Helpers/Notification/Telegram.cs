using System;
using System.Net;

namespace Toyota.Helpers.Notification
{
    public static class Telegram
    {
        public static String Token = "";
        private static String chatId = "870452692";

        public static bool Send(String message)
        {
            String url = $"https://api.telegram.org/bot{Token}/sendMessage?chat_id={chatId}&text={message}";
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
