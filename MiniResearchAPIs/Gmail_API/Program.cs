using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gmail_API
{
    internal class Program
    {
        private static UserCredential Login(string googleClientId, string googleClientSecret, string[] scopes)
        {
            ClientSecrets secrets = new ClientSecrets()
            {
                ClientId = googleClientId,
                ClientSecret = googleClientSecret
            };

            return GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, scopes, "user", CancellationToken.None).Result;
        }



        static void Main(string[] args)

        {

            string googleClientId = "Enter Client ID";
            string googleClientSecret = "Enter Client Secret";
            string[] scopes = new[] { Google.Apis.Gmail.v1.GmailService.Scope.GmailReadonly };

            UserCredential credential = Login(googleClientId, googleClientSecret, scopes);

            using (var gmailService = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = credential }))
            {
                var profile = gmailService.Users.GetProfile("me").Execute();
                Console.WriteLine(profile.EmailAddress);
                Console.WriteLine(profile.MessagesTotal);

            }
            EmailGetter emailGetter = new EmailGetter(credential);
            var messages = emailGetter.GetMessages("me");

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
        private static string GetMessageBody(Message message)
        {
            string body = "";

            if (message.Payload.Body != null && message.Payload.Body.Data != null)
            {
                body = DecodeBody(message.Payload.Body);
            }
            else if (message.Payload.Parts != null)
            {
                foreach (var part in message.Payload.Parts)
                {
                    if (part.MimeType == "text/plain" && part.Body != null && part.Body.Data != null)
                    {
                        body = DecodeBody(part.Body);
                        break;
                    }
                }
            }

            return body;
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
    }
}
