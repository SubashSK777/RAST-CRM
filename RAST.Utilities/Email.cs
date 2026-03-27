using System;
using System.Net.Mail;
using System.Configuration;

namespace RAST.Utilities
{
    public class Email
    {
        private MailMessage m;

        public Email()
        {
            m = new MailMessage();
            m.Sender = m.From = new MailAddress(ConfigurationManager.AppSettings["FromEmailAddress"].ToString());
            m.IsBodyHtml = true;
        }

        public bool isHTML
        {
            set
            {
                m.IsBodyHtml = value;
            }
        }

        public string to
        {
            set
            {
                m.To.Add(value);
            }
        }

        public string from
        {
            set
            {
                m.Sender = m.From = new MailAddress(value);
            }
        }
        public string subject
        {
            set
            {
                m.Subject = value;
            }
        }

        public string body
        {
            get
            {
                return body;
            }
            set
            {
                m.Body = value;

            }
        }

        public void send()
        {
            try
            {
                SmtpClient sc = new SmtpClient();
                sc.Host = ConfigurationManager.AppSettings["SMTPHost"].ToString();
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Port = Convert.ToInt32( ConfigurationManager.AppSettings["SMTPPort"]);
                sc.Credentials = new System.Net.NetworkCredential(
                        ConfigurationManager.AppSettings["SMTPUserName"].ToString(), 
                        ConfigurationManager.AppSettings["SMTPUserPassword"].ToString());


                sc.Send(m);
            }
            catch (Exception e)
            {
                //Log error somehow
            }
        }
    }
}
