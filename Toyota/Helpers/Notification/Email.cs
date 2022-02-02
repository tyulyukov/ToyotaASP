using System;
using MailKit.Net.Smtp;
using MimeKit;

namespace Toyota.Helpers.Notification
{
    public static class Email
    {
        public static bool Send(String text, String subject, String email)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Admin", "makstyulyukov@ukr.net");
            message.From.Add(from);
            
            MailboxAddress to = new MailboxAddress("", email);
            message.To.Add(to);

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = text;

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
