using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using PigeonMailer.Helpers;

namespace PigeonMailer.Lib
{
    public class EmailTemplate
    {
        private string bodyFilePath;
        public string BodyFilePath
        {
            get { return this.bodyFilePath; }
            set 
            { 
                this.bodyFilePath = value;
                ReadBodyFile();
            }
        }
        private void ReadBodyFile()
        {
            Body = File.ReadAllText(BodyFilePath);
        }

        public string Body { get; set; }
        public string Subject { get; set; }
        public Boolean IsHtml { get; set; }
        public Boolean EnableSsl { get; set; }
        public string FromAddress { get; set; }
        public string FromName { get; set; }

        private MailAddressCollection toAddress;
        public MailAddressCollection ToAddress
        {
            get 
            {
                if (this.toAddress == null)
                {
                    this.toAddress = new MailAddressCollection();
                }
                return this.toAddress; 
            }
        }

        private Dictionary<String, Object> parameters;
        public Dictionary<String, Object> Parameters
        {
            get
            {
                if (this.parameters == null)
                {
                    this.parameters = new Dictionary<string, object>();
                }
                return this.parameters;
            }
        }

        public void ProcessTemplate()
        {
            foreach (String key in this.parameters.Keys)
            {
                Object value = this.parameters[key];
                this.Body = this.Body.Replace(key, value.ToString());
                this.Subject = this.Subject.Replace(key, value.ToString());
            }
        }

        private Object entityInstance;
        public void ProcessTemplate(Object entityInstance)
        {
            this.entityInstance = entityInstance;
            Regex regex = new Regex("{(.+?)}", RegexOptions.Singleline);
            MatchEvaluator matchEvaluator = new MatchEvaluator(GetValues);
            this.Body = regex.Replace(this.Body, matchEvaluator);
            if (this.Subject != null)
            {
                this.Subject = regex.Replace(this.Subject, matchEvaluator);
            }
        }

        private String GetValues(Match match)
        {
            object obj = ReflectionHelper.GetPropertyValue(this.entityInstance, match.Groups[1].Value);
            String valor = obj != null ? obj.ToString() : string.Empty;
            if (string.IsNullOrEmpty(valor)) return null;
            return valor.Replace(Environment.NewLine, "<br />" + Environment.NewLine);
        }
    }
}
