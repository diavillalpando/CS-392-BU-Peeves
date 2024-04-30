using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Gmail.v1.GmailService;

namespace Gmail_API
{
    internal class Program
    {
        private static UserCredential Authorize(string clientId, string clientSecret, string[] scopes)
        {
            // GoogleWebAuthorizationBroker.RevokeTokenAsync(new ClientSecrets { ClientId = clientId, ClientSecret = clientSecret }, CancellationToken.None).Wait();

            // Initiate authorization flow
            return GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets { ClientId = clientId, ClientSecret = clientSecret },
                scopes,
                "user",
                CancellationToken.None,
                new FileDataStore("Gmail.API.Auth.Store")).Result;
        }


        static void Main(string[] args)

        {

            string googleClientId = "333907828203-btcnk4m5q6ou2gv7620e4vgaq0i9p3rm.apps.googleusercontent.com";
            string googleClientSecret = "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";
            string[] scopes = new[] {
    GmailService.ScopeConstants.GmailReadonly,
    GmailService.ScopeConstants.GmailCompose,
    GmailService.ScopeConstants.GmailSend
};



            UserCredential credential = Authorize(googleClientId, googleClientSecret, scopes);

            using (var gmailService = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = credential }))
            {
                var profile = gmailService.Users.GetProfile("me").Execute();
                Console.WriteLine(profile.EmailAddress);
                Console.WriteLine(profile.MessagesTotal);

            }
            EmailGetter emailGetter = new EmailGetter(credential);
            var messages = emailGetter.GetMessages("me");

            //var msg = new MimeKit.MimeMessage();
            //msg.From.Add(new MimeKit.MailboxAddress("Sender Name", "sycds460@gmail.com"));
            //msg.To.Add(new MimeKit.MailboxAddress("Recipient Name", "songyuchen2584@gmail.com"));
            //msg.Subject = "Testing email sending via Gmail API";
            //msg.Body = new MimeKit.TextPart("plain")
            //{
            //    Text = "This is a test email sent via the Gmail API."
            //};

            //// Convert MimeMessage to RFC822 formatted string
            //string rfc822Message = msg.ToString();

            //try
            //{

            //    using (var gmailService = new GmailService(new BaseClientService.Initializer
            //    {
            //        HttpClientInitializer = credential
            //    }))
            //    {

            //        var message = new Message
            //        {
            //            Raw = Base64UrlEncode(rfc822Message)
            //        };

            //        gmailService.Users.Messages.Send(message, "me").Execute();
            //        Console.WriteLine("Email sent successfully.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //}

            int i = 0;
            foreach (var message in messages)
            {

                Console.WriteLine(DateTime.FromBinary((long)message.InternalDate));
                foreach (var header in message.Payload.Headers)
                    Console.WriteLine(header.Name + "\t" + header.Value);
                string messageBody = GetMessageBody(message);
                Console.WriteLine("Message Body:");
                Console.WriteLine(messageBody);
                Console.WriteLine("_____________________________________________________________");
                i++;
                if (i >= 3)
                {
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                }
            }

        }
        public static string GetMessageBody(Message message)
        {
            return GetPartBody(message.Payload);
        }

        private static string GetPartBody(MessagePart part)
        {
            if (part.Body != null && !string.IsNullOrEmpty(part.Body.Data))
            {
                return DecodeBody(part.Body);
            }
            else if (part.Parts != null)
            {
                foreach (var childPart in part.Parts)
                {
                    string body = GetPartBody(childPart);
                    if (!string.IsNullOrEmpty(body))
                    {
                        return body;
                    }
                }
            }

            return null;
        }



        private static string DecodeBody(MessagePartBody body)
        {
            string data = body.Data;
            byte[] dataBytes = FromBase64Url(data);
            return Encoding.UTF8.GetString(dataBytes);
        }

        private static byte[] FromBase64Url(string base64Url)
        {
            string paddedBase64 = base64Url.Length % 4 == 0 ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            string base64 = paddedBase64.Replace("_", "/").Replace("-", "+");
            return Convert.FromBase64String(base64);
        }

        private static string Base64UrlEncode(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }
    }
}
