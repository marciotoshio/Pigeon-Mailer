using System.Net.Mail;
using PigeonMailer.Lib;

namespace PigeonMailer.FluentInterface
{
    public class FluentEmailSender
    {
        private Sender sender;
        private EmailTemplate emailTemplate;
        
        public FluentEmailSender()
        {
            this.sender = new Sender();
            this.emailTemplate = new EmailTemplate();
        }

        #region properties
        public FluentEmailSender BodyFilePath(string value)
        {
            this.emailTemplate.BodyFilePath = value;
             return this;
        }

        public FluentEmailSender Body(string value)
        {
            this.emailTemplate.Body = value;
            return this;
        }

        public FluentEmailSender Subject(string value)
        {
            this.emailTemplate.Subject = value;
            return this;
        }

        public FluentEmailSender IsHtml()
        {
            this.emailTemplate.IsHtml = true;
            return this;
        }

        public FluentEmailSender EnableSsl()
        {
            this.emailTemplate.EnableSsl = true;
            return this;
        }

        public FluentEmailSender FromAddress(string value)
        {
            this.emailTemplate.FromAddress = value;
            return this;
        }

        public FluentEmailSender FromName(string value)
        {
            this.emailTemplate.FromName = value;
            return this;
        }

        public FluentEmailSender AddToAddress(MailAddress value)
        {
            this.emailTemplate.ToAddress.Add(value);
            return this;
        }

        public FluentEmailSender AddToAddress(string value)
        {
            this.emailTemplate.ToAddress.Add(value);
            return this;
        }
        #endregion

        public FluentEmailSender AddParameter(string key, string value)
        {
            this.emailTemplate.Parameters.Add(key, value);
            return this;
        }

        public FluentEmailSenderFinal ProcessTemplate()
        {
            this.emailTemplate.ProcessTemplate();
            return new FluentEmailSenderFinal(this.sender, this.emailTemplate);
        }
        
        public FluentEmailSenderFinal ProcessTemplate(object instance)
        {
            this.emailTemplate.ProcessTemplate(instance);
            return new FluentEmailSenderFinal(this.sender, this.emailTemplate);
        }

        public class FluentEmailSenderFinal
        {
            private EmailTemplate emailTemplate;
            private Sender sender;
            public FluentEmailSenderFinal(Sender sender, EmailTemplate emailTemplate)
            {
                this.emailTemplate = emailTemplate;
                this.sender = sender;
            }

            public void Send()
            {
                this.sender.SendMessage(this.emailTemplate);
            }
        }
    }
}
