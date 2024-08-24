using Customer.Core.Interfaces.Common;
using Customer.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Common
{
    public class EmailService : IEmailService
    {
        public Result<object> Send(EmailToDTO emailModel)
        {
            try
            {
                // SMTP server settings
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587; // or the appropriate port for your SMTP server
                string smtpUsername = "amr.salah110495@gmail.com";
                string smtpPassword = "sgyugjgtktwtfndx";

                // Sender and recipient email addresses
                //string recipientEmail = emailModel.EmailTo;

                // Create a new MailMessage object
                MailMessage message = new MailMessage();

                MailAddress fromMail = new MailAddress(smtpUsername);
                message.From = fromMail;
                emailModel.EmailTo.ForEach(mail =>
                {
                    message.To.Add(new MailAddress(mail));
                });

                message.Subject = emailModel.Subject;
                message.Body = emailModel.Body;

                // Create a new SmtpClient object
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(smtpServer, smtpPort);
                smtpClient.EnableSsl = true; // Enable SSL/TLS if required
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                // Send the email
                smtpClient.Send(message);
                return new Result<object>() { Status = true, Data = null, Message = "Email sent successfully" };

            }
            catch (Exception ex)
            {
                return new Result<object>() { Status = false, Data = null, Message = "Some thing went wrong", ExceptionMessage = ex.Message };
               

            }
        }
    }
}
