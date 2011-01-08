using System.Net.Mail;

namespace PigeonMailer.Lib
{
    public class Sender
    {
        private EmailTemplate template;
        public void SendMessage(EmailTemplate template)
        {
            this.template = template;
            var message = CreateMessage();
            new SmtpClient().Send(message);
        }

        private MailMessage CreateMessage()
        {
            var message = new MailMessage();
            message.Body = this.template.Body;
            message.Subject = this.template.Subject;
            foreach (var address in this.template.ToAddress)
            {
                message.To.Add(address);
            }
            if (this.template.FromAddress != null)
            {
                message.From = new MailAddress(this.template.FromAddress, this.template.FromName);
            }
            message.IsBodyHtml = this.template.IsHtml;
            return message;
        }
    }
}