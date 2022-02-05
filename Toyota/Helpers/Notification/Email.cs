using System;
using MailKit.Net.Smtp;
using MimeKit;
using Toyota.Data.Entities;

namespace Toyota.Helpers.Notification
{
    public static class Email
    {
        public static bool Send(CallBack callback, String subject, String email)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Admin", "makstyulyukov@ukr.net");
            message.From.Add(from);
            
            MailboxAddress to = new MailboxAddress("", email);
            message.To.Add(to);

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<div style='width: 100%; padding: 20px; background-color: gray;'>" +
                                   $"   <img style='width: 70px;' src='https://www.pngall.com/wp-content/uploads/2016/04/Toyota-Logo-PNG-Image.png'>" +
                                   $"</div>" +
                                   $"<div style='width: 100%; padding: 20px; background-color: #D6E7F0;'>" +
                                   $"   Заявка на звонок от <strong>{callback.Name}</strong><br>" +
                                   $"   Номер телефона: <strong>{callback.Phone}</strong><br>" +
                                   $"   ID заявки: {callback.Id}" +
                                   $"</div>" +
                                   $"<div style='width: 100%; padding: 20px; background-color: gray;'>" +
                                   $"© 2022 Toyota, Inc. All rights reserved." +
                                   $"</div>";

            message.Subject = subject;
            message.Body = bodyBuilder.ToMessageBody();

            using(var client = new SmtpClient())
            {
                client.Connect("smtp.ukr.net", 465, true);
                client.Authenticate("makstyulyukov@ukr.net", "w7Nl4jZmWTQxeS9S");

                client.Send(message);
                client.Disconnect(true);
            }

            return true;
        }
    }
}
