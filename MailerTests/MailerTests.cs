using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PigeonMailer.Lib;
using System.IO;

namespace MailerTests
{
    [TestClass]
    public class MailerTests
    {
        private string GetBasePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..");
        }

        [TestMethod]
        public void can_read_a_file()
        {
            var template = new EmailTemplate();
            template.BodyFilePath = Path.Combine(GetBasePath(), @"Resources/TextForTest.txt");
            var content = template.Body;
            Assert.AreEqual("This is a test file.", content);
        }

        [TestMethod]
        public void can_add_mail_addresses()
        {
            var template = new EmailTemplate();
            template.ToAddress.Add("test1@email.com");
            template.ToAddress.Add("test2@email.com");
            Assert.AreEqual(2, template.ToAddress.Count);
        }

        [TestMethod]
        public void can_add_parameters()
        {
            var template = new EmailTemplate();
            template.Parameters.Add("{name}", "Toshio");
            template.Parameters.Add("{age}", 27);
            Assert.AreEqual(2, template.Parameters.Count);
        }

        [TestMethod]
        public void process_a_template_with_keys_in_the_body_over_parameters()
        {
            var template = new EmailTemplate();
            template.Subject = "Test Subject";
            //First read a template
            template.BodyFilePath = Path.Combine(GetBasePath(), @"Resources/TemplateForTest.txt");
            //checks whether it contains the keys
            Assert.IsTrue(template.Body.Contains("{name}"));
            Assert.IsTrue(template.Body.Contains("{age}"));
            //add the parameters that match the keys
            template.Parameters.Add("{name}", "Toshio");
            template.Parameters.Add("{age}", 27);
            //process the template with parameters
            template.ProcessTemplate();
            //checks if the keys have been exchanged with the corresponding values
            Assert.IsTrue(template.Body.Contains("Toshio"));
            Assert.IsTrue(template.Body.Contains("27"));
        }

        [TestMethod]
        public void process_a_template_with_keys_in_the_subject_over_parameters()
        {
            var template = new EmailTemplate();
            template.Subject = "Hello {name}!";
            template.Body = "Body test";
            //add the parameters that match the keys
            template.Parameters.Add("{name}", "Toshio");
            //process the template with parameters
            template.ProcessTemplate();
            //checks if the keys have been exchanged with the corresponding values
            Assert.IsTrue(template.Subject.Contains("Toshio"));
        }

        [TestMethod]
        public void process_a_template_with_keys_in_the_body_over_an_object()
        {
            var template = new EmailTemplate();
            template.Subject = "Test Subject";
            //First read a template
            template.BodyFilePath = Path.Combine(GetBasePath(), @"Resources/TemplateForTest.txt");
            //checks whether it contains the keys
            Assert.IsTrue(template.Body.Contains("{name}"));
            Assert.IsTrue(template.Body.Contains("{age}"));
            //creates an object with properties that match the keys
            var obj = new { name = "Toshio", age = 27 };
            //process the template with an object
            template.ProcessTemplate(obj);
            //checks if the keys have been exchanged with the corresponding values
            Assert.IsTrue(template.Body.Contains("Toshio"));
            Assert.IsTrue(template.Body.Contains("27"));
        }

        [TestMethod]
        public void process_a_template_with_keys_in_the_subject_over_an_object()
        {
            var template = new EmailTemplate();
            template.Subject = "Hello {name}!";
            template.Body = "Body test";
            //creates an object with properties that match the keys
            var obj = new { name = "Toshio" };
            //process the template with an object
            template.ProcessTemplate(obj);
            //checks if the keys have been exchanged with the corresponding values
            Assert.IsTrue(template.Subject.Contains("Toshio"));
        }
    }
}
