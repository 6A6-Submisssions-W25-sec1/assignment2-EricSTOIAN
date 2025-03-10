using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MauiEmail.Models;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEmail
{
    public class EmailService : IEmailService
    {
        MailConfig mailConfig;
        public EmailService(MailConfig config)
        {
            mailConfig = config;
        }

        public async Task DeleteMessageAsync(UniqueId uid)
        {
            using (var clientReceiver = new ImapClient())
            {
                if (!clientReceiver.IsConnected)
                    await clientReceiver.ConnectAsync(mailConfig.ReceiveHost, mailConfig.ReceivePort, mailConfig.ReceiveSocketOptions);

                if (!clientReceiver.IsAuthenticated)
                {
                    await clientReceiver.AuthenticateAsync(mailConfig.EmailAddress, mailConfig.Password); // the password here is the generated app password for the 2-step authentication in google.
                }
                var inbox = clientReceiver.Inbox;
                inbox.Open(FolderAccess.ReadWrite);

                inbox.Store(uid, new StoreFlagsRequest(StoreAction.Add, MessageFlags.Deleted) { Silent = true });
                inbox.Expunge();
            }
        }

        public async Task DisconnectRetreiveClientAsync()
        {
            using (var clientReceiver = new ImapClient())
            {
                await clientReceiver.DisconnectAsync(true);
            }
        }

        public async Task DisconnectSendClientAsync()
        {
            using (var clientSender = new SmtpClient())
            {
                await clientSender.DisconnectAsync(true);
            }
        }

        public async Task<IEnumerable<MimeMessage>> DownloadAllEmailsAsync()
        {
            List<MimeMessage> message = new List<MimeMessage>();
            using (var clientReceiver = new ImapClient())
            {
                if (!clientReceiver.IsConnected)
                    await clientReceiver.ConnectAsync(mailConfig.ReceiveHost, mailConfig.ReceivePort, mailConfig.ReceiveSocketOptions);

                if (!clientReceiver.IsAuthenticated)
                {
                    await clientReceiver.AuthenticateAsync(mailConfig.EmailAddress, mailConfig.Password); // the password here is the generated app password for the 2-step authentication in google.
                }
                var inbox = clientReceiver.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                Console.WriteLine("Total messages: {0}", inbox.Count);
                Console.WriteLine("Recent messages: {0}", inbox.Recent);

                for (int i = 0; i < inbox.Count; i++)
                {
                    message.Add(inbox.GetMessage(i));
                    // Console.WriteLine("Subject: {0}", message.Subject);
                }
            } 
            return message;
        }

        public async Task SendMessageAsync(MimeMessage message)
        {
            using (var clientSender = new SmtpClient())
            {
                if (!clientSender.IsConnected)
                    await clientSender.ConnectAsync(mailConfig.SendHost, mailConfig.SendPort, mailConfig.SendSocketOptions);
                //if (client.AuthenticationMechanisms.Contains("OAUTHBEARER") || client.AuthenticationMechanisms.Contains("XOAUTH2"))
                //AuthenticateAsync(client).GetAwaiter().GetResult();
                if (!clientSender.IsAuthenticated)
                    await clientSender.AuthenticateAsync(mailConfig.EmailAddress, mailConfig.Password); // the password here is the generated app password for the 2-step authentication in google.
                clientSender.Send(message);
            }
        }

        public async Task StartRetreiveClientAsync()
        {
            using (var clientReceiver = new ImapClient())
            {
                if (!clientReceiver.IsConnected)
                    await clientReceiver.ConnectAsync(mailConfig.ReceiveHost, mailConfig.ReceivePort, mailConfig.ReceiveSocketOptions);

                if (!clientReceiver.IsAuthenticated)
                {
                    await clientReceiver.AuthenticateAsync(mailConfig.EmailAddress, mailConfig.Password); // the password here is the generated app password for the 2-step authentication in google.
                }
            }
        }

        public async Task StartSendClientAsync()
        {
            using (var clientSender = new SmtpClient())
            {
                if (!clientSender.IsConnected)
                    await clientSender.ConnectAsync(mailConfig.SendHost, mailConfig.SendPort, mailConfig.SendSocketOptions);
                //if (client.AuthenticationMechanisms.Contains("OAUTHBEARER") || client.AuthenticationMechanisms.Contains("XOAUTH2"))
                //AuthenticateAsync(client).GetAwaiter().GetResult();
                if (!clientSender.IsAuthenticated)
                    await clientSender.AuthenticateAsync(mailConfig.EmailAddress, mailConfig.Password); // the password here is the generated app password for the 2-step authentication in google.
            }
        }

        public async Task<IEnumerable<ObservableMessage>?> FetchAllMessages()
        {
            List<ObservableMessage> messageList = new List<ObservableMessage>();
            
            using (var clientReceiver = new ImapClient())
            {
                if (!clientReceiver.IsConnected)
                    await clientReceiver.ConnectAsync(mailConfig.ReceiveHost, mailConfig.ReceivePort, mailConfig.ReceiveSocketOptions);

                if (!clientReceiver.IsAuthenticated)
                {
                    await clientReceiver.AuthenticateAsync(mailConfig.EmailAddress, mailConfig.Password); // the password here is the generated app password for the 2-step authentication in google.
                }
                var inbox = clientReceiver.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                foreach (var summary in inbox.Fetch(0, -1, MessageSummaryItems.Envelope))
                {
                    //Console.WriteLine("[summary] {0:D2}: {1}", summary.Index, summary.Envelope.Subject);
                    ObservableMessage message = new ObservableMessage(summary);
                    messageList.Add(message);
                }
            }
            return messageList;
        }

        public async Task<ObservableMessage?> GetMessageAsync(MailKit.UniqueId id)
        {
            List<MailKit.UniqueId> ids = new List<MailKit.UniqueId>();
            ids.Add(id);

            using (var clientReceiver = new ImapClient())
            {
                if (!clientReceiver.IsConnected)
                    await clientReceiver.ConnectAsync(mailConfig.ReceiveHost, mailConfig.ReceivePort, mailConfig.ReceiveSocketOptions);

                if (!clientReceiver.IsAuthenticated)
                {
                    await clientReceiver.AuthenticateAsync(mailConfig.EmailAddress, mailConfig.Password); // the password here is the generated app password for the 2-step authentication in google.
                }
                var inbox = clientReceiver.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadOnly);

                MimeMessage summary = (MimeMessage)await inbox.FetchAsync(ids, MessageSummaryItems.Envelope);

                ObservableMessage message = new ObservableMessage(summary, id);
                return message;
            }
        }

    }
}
