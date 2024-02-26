using MimeKit;
using System.Net.Mail;
using System.Text.Json;
using MailKit.Net.Smtp;

namespace JuneDev_Mailkit_Example
{
    public class MailHandler
    {
        private string? _email;
        private string? _password;
        private string? _server;
        private int? _port;
        private string? _name;

        public MailHandler()
        {

        }

        public MailHandler(string email, string password, string server, int port, string name)
        {
            _email = email;
            _password = password;
            _server = server;
            _port = port;
            _name = name;
        }

        public string? Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string? Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string? Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public int? Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public string? Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public async Task<bool> SendEmail(string subject, string content, string recvEmail, string? recvName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_name, _email));
            message.To.Add(new MailboxAddress(recvName, recvEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = content
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(_server, _port ?? 587, false); // if use ssl true; otherwise, false
                client.AuthenticationMechanisms.Remove("XOAUTH2"); // Disable OOAUTH2
                client.Authenticate(_email, _password); // authenticate with the server (throws exception if failed)
                await client.SendAsync(message); // send email
                client.Disconnect(true); // disconnect from server
            }

            return true;
        }


        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
