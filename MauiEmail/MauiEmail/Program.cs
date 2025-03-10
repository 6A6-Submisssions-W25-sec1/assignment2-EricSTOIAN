
//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Auth.OAuth2.Flows;
//using Google.Apis.Util.Store;
using MailKit.Net.Imap;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using Org.BouncyCastle.Crypto;
using MailKit;

namespace MauiEmail
{
    //internal class Program
    //{
    //    const string GMailAccount = "jacabbott123@gmail.com";
    //    static async Task Main(string[] args)
    //    {
    //        try
    //        {
    //            MailConfig config = new MailConfig("jacabbott123@gmail.com", "shzx xpyr zvsc uxpf", "imap.gmail.com", SecureSocketOptions.SslOnConnect, 993, "smtp.gmail.com", 587, SecureSocketOptions.StartTls, "", "", "");

    //            IEmailService email = new EmailService(config);

    //            var message = new MimeMessage();
    //            message.From.Add(new MailboxAddress("john abbott", "jacabbott@gmail.com"));
    //            message.To.Add(new MailboxAddress("john abbott", "estomtl@gmail.com"));
    //            message.Subject = "How you doin'?";

    //            message.Body = new TextPart("plain")
    //            {
    //                Text = @"Hey E, I just wanted to let you know that Javid and I were going to go play some paintball, you in? -- Ant"
    //            };

    //            await email.StartRetreiveClientAsync();
    //            await email.StartSendClientAsync();
    //            await email.SendMessageAsync(message);
    //            await email.DeleteMessageAsync(346);
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine(ex.ToString());
    //        }

    //    }
    //}
}
