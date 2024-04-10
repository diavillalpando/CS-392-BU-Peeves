using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmail_API
{
    internal class EmailGetter
    {
        private UserCredential _credential;

        public EmailGetter(UserCredential credential)
        {
            _credential = credential;
        }

        public IEnumerable<Message> GetMessages(string userId, string[] labels = null, bool includeSpamAndTrash = false)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");
            using (var gmailService = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = _credential }))
            {
                var listMessagesRequest = gmailService.Users.Messages.List(userId);
                listMessagesRequest.IncludeSpamTrash = includeSpamAndTrash;
                if (labels != null)
                    listMessagesRequest.LabelIds = labels;

                bool hasNext = true;
                while (hasNext)
                {
                    var messages = listMessagesRequest.Execute();
                    hasNext = messages.NextPageToken != null;

                    foreach (var message in messages.Messages)
                        yield return GetSingleMessage(userId, message.Id, gmailService);
                    if (hasNext)
                        listMessagesRequest.PageToken = messages.NextPageToken;
                }
            }
        }
        private Message GetSingleMessage(string userId, string messageId, GmailService service)
        {
            var getSingleMessageRequest = service.Users.Messages.Get("me", messageId);
            getSingleMessageRequest.Format = UsersResource.MessagesResource.GetRequest.FormatEnum.Full;
            getSingleMessageRequest.MetadataHeaders = new[] { "From", "To" };

            return getSingleMessageRequest.Execute();
        }
    }
}
