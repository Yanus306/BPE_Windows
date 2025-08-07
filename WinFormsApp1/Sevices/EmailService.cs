using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using WinFormsApp1.Models;
using System.Collections.Generic;

namespace WinFormsApp1.Services
{
    public class EmailService
    {
        public void TestLogin(string email, string password)
        {
            using var client = new ImapClient();
            client.Connect("imap.gmail.com", 993, true);
            client.Authenticate(email, password);
            client.Disconnect(true);
        }

        public List<EmailMessage> FetchInbox(string email, string password)
        {
            var messages = new List<EmailMessage>();
            using var client = new ImapClient();
            client.Connect("imap.gmail.com", 993, true);
            client.Authenticate(email, password);

            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            var uids = inbox.Search(SearchQuery.All);
            foreach (var uid in uids.Reverse().Take(20))
            {
                var message = inbox.GetMessage(uid);
                var emailMessage = new EmailMessage
                {
                    From = message.From.ToString(),
                    Subject = message.Subject,
                    Body = message.TextBody,
                    Date = message.Date.DateTime
                };
                
                // 메일 헤더 정보 저장
                emailMessage.Headers = ExtractHeaders(message);
                
                messages.Add(emailMessage);
            }

            client.Disconnect(true);
            return messages;
        }

        private Dictionary<string, string> ExtractHeaders(MimeMessage message)
        {
            var headers = new Dictionary<string, string>();
            
            foreach (var header in message.Headers)
            {
                headers[header.Field] = header.Value;
            }
            
            return headers;
        }
    }
}
