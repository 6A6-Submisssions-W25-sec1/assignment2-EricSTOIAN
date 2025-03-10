using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEmail.Models
{
    public class ObservableMessage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private MailKit.UniqueId _uniqueId;
        private DateTimeOffset _date;
        private string _subject = "";
        private string _body = "";
        private string _htmlBody = "";
        private MimeKit.MailboxAddress? _from;
        private List<MailboxAddress> _to = new List<MailboxAddress>();
        private bool _isRead;
        private bool _isFavorite;

        public MailKit.UniqueId UniqueId
        {
            get { return _uniqueId; }
            set
            {
                _uniqueId = value;
            }
        }
        public DateTimeOffset Date
        {
            get { return _date; }
            set
            {
                _date = value;
            }
        }
        public string Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
            }
        }
        public string Body
        {
            get { return _body; }
            set
            {
                _body = value;
            }
        }
        public string HtmlBody
        {
            get { return _htmlBody; }
            set
            {
                _htmlBody = value;
            }
        }
        public MimeKit.MailboxAddress From
        {
            get { return _from; }
            set
            {
                _from = value;
            }
        }
        public List<MailboxAddress> To
        {
            get { return _to; }
            set
            {
                _to = value;
            }
        }
        public bool IsRead
        {
            get { return _isRead; }
            set
            {
                _isRead = value;
            }
        }
        public bool IsFavorite
        {
            get { return _isFavorite; }
            set
            {
                _isFavorite = value;
            }
        }

        public ObservableMessage(IMessageSummary message)
        {
            _uniqueId = message.UniqueId;
            _date = message.Date;
            _subject = message.Envelope.Subject;
            _body = null;
            _htmlBody = null;
            _from = (MailboxAddress)message.Envelope.From[0];
            foreach(MailboxAddress to in message.Envelope.To)
            {
                _to.Add(to);
            }
            _isRead = (message.Flags == MessageFlags.Seen);
            _isFavorite = (message.Flags == MessageFlags.Flagged);
        }

        public ObservableMessage(MimeMessage mimeMessage, UniqueId uniqueId)
        {
            _uniqueId = uniqueId;
            _date = mimeMessage.Date;
            _subject = mimeMessage.Subject;
            _body = mimeMessage.TextBody;
            _htmlBody = mimeMessage.HtmlBody;
            _from = (MailboxAddress)mimeMessage.From[0];
            foreach (MailboxAddress to in mimeMessage.To)
            {
                _to.Add(to);
            }
            _isRead = false; //maybe have to change these 2 to something else
            _isFavorite = false;
        }

        public MimeMessage ToMime()
        {
            MimeMessage mimeMessage = new MimeMessage(this.From, this.To, this.Subject, this.Body);

            mimeMessage.Date = this.Date;
            mimeMessage.Subject = this.Subject;

            return mimeMessage;
        }

        public ObservableMessage GetForward() //I have no idea if this is even good.
        {
            this.Subject = $"FW:{this.Subject}";
            this.HtmlBody = $"<p>----Forwarded Message----</p><br></br><p>From: {this.From}</p>" +
                $"<br></br> <p>To: {this.To}</p> <br></br> <p>{this.Body}</p>";

            return this;
        }
    }
}
