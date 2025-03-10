using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEmail
{
    public class MailConfig : IEMailConfig
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string ReceiveHost { get; set; }

        public SecureSocketOptions ReceiveSocketOptions { get; set; }

        public int ReceivePort { get; set; }

        public string SendHost { get; set; }

        public int SendPort { get; set; }

        public SecureSocketOptions SendSocketOptions { get; set; }

        public string OAuth2ClientId { get; set; }

        public string OAuth2ClientSecret { get; set; }

        public string OAuth2RefreshToken { get; set; }

        public MailConfig(string emailAddress, string password, string receiveHost, SecureSocketOptions receiveSocketOptions, int receivePort, string sendHost, int sendPort, SecureSocketOptions sendSocketOptions, string oAuth2ClientId, string oAuth2ClientSecret, string oAuth2RefreshToken) 
        {
            EmailAddress = emailAddress;
            Password = password;
            ReceiveHost = receiveHost;
            ReceiveSocketOptions = receiveSocketOptions;
            ReceivePort = receivePort;
            SendHost = sendHost;
            SendPort = sendPort;
            SendSocketOptions = sendSocketOptions;
            OAuth2ClientId = oAuth2ClientId;
            OAuth2ClientSecret = oAuth2ClientSecret;
            OAuth2RefreshToken = oAuth2RefreshToken;
        }
    }
}
