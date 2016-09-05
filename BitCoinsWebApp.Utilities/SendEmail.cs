namespace BitCoinsWebApp.Utilities
{
    using BitCoinsWebApp.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Xml;

    public class SendEmail
    {
        private string FromAddress;        
        private string strSmtpClient;
        private string UserID;
        private string Password;
        private string ReplyTo;
        private string SMTPPort;
        private Boolean bEnableSSL;

        private void InitMail()
        {
            FromAddress = ConfigurationManagerKey.FromAddress;          
            //strSmtpClient = System.Configuration.ConfigurationManager.AppSettings.Get("SmtpClient");
            strSmtpClient = ConfigurationManagerKey.strSmtpClient;
            UserID = ConfigurationManagerKey.UserID; ;
            Password = ConfigurationManagerKey.Password;
            ReplyTo = ConfigurationManagerKey.ReplyTo;
            SMTPPort = ConfigurationManagerKey.SMTPPort;
            if (ConfigurationManagerKey.SSL == "YES")
            {
                bEnableSSL = true;
            }
            else
            {
                bEnableSSL = false;
            }
        }

        public void SendMail(string messageId, string toAddress,string mailFormatxml, string[] param)
        {
            XmlDocument xdoc = new XmlDocument();          
            string subject = "";
            string body = "";
            XmlNode mailNode = default(XmlNode);
            int n = 0;

            if ((System.IO.File.Exists(mailFormatxml)))
            {
                xdoc.Load(mailFormatxml);
                mailNode = xdoc.SelectSingleNode("MailFormats/MailFormat[@Id='" + messageId + "']");
                subject = mailNode.SelectSingleNode("Subject").InnerText;
                body = mailNode.SelectSingleNode("Body").InnerText;
                if ((param == null))
                {
                    throw new Exception("Mail format file not found.");
                }
                else
                {
                    for (n = 0; n <= param.Length - 1; n++)
                    {
                        body = body.Replace(n.ToString() + "?", param[n]);
                        subject = subject.Replace(n.ToString() + "?", param[n]);
                    }
                }

                InitMail();

                dynamic MailMessage = new MailMessage();
                MailMessage.From = new MailAddress(FromAddress);
                MailMessage.To.Add(toAddress);
                MailMessage.Subject = subject;
                MailMessage.IsBodyHtml = true;
                MailMessage.Body = body;

                MailMessage.ReplyTo = new MailAddress(FromAddress);

                SmtpClient SmtpClient = new SmtpClient();
                SmtpClient.Host = strSmtpClient;
                SmtpClient.EnableSsl = bEnableSSL;
                SmtpClient.Port = Convert.ToInt32(SMTPPort);
                SmtpClient.UseDefaultCredentials = true;
                SmtpClient.Credentials = new System.Net.NetworkCredential(UserID, Password);
                try
                {
                    SmtpClient.Send(MailMessage);
                }
                catch (SmtpFailedRecipientsException ex)
                {
                    for (int i = 0; i <= ex.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                        if ((status == SmtpStatusCode.MailboxBusy) | (status == SmtpStatusCode.MailboxUnavailable))
                        {
                            System.Threading.Thread.Sleep(5000);
                            SmtpClient.Send(MailMessage);
                        }
                    }
                }
            }
        }
        public void SendMailRegister(string messageId, string toAddress, string[] param) 
        {
            SendEmail sendMails = new SendEmail();
            string mailFormatxml = HttpContext.Current.Server.MapPath("\\") + "MailFormats\\RegisterSuccess.xml";
            sendMails.SendMail( messageId, toAddress,mailFormatxml,param);
        }
    }
}
