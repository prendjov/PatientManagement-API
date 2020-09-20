using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using PatientManagement.API.Models;
using PatientManagementDTO.DTO;
using PatientManagement.API.Security;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace PatientManagement.API.Services
{
    public class EmailSender
    {
        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }
        public async static Task<bool> SendEmailToPatient(AppointmentInfoDTO appointment,   string patientEmail)
        {
            //SmtpClient client = new SmtpClient();
            //client.Port = 587;
            //client.Host = "smtp.gmail.com";
            //client.EnableSsl = true;
            //client.Timeout = 60000;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new System.Net.NetworkCredential("prendjov.p21@gmail.com", EncryptionHelper.Decrypt("Ch1XbaLdPZwmnXxcwtrQ5A=="));

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("prendjov.p21@gmail.com", "Pance Prendjov");
            message.To.Add(new MailAddress(patientEmail));
            message.Subject = "Upcoming appointment reminder";
            message.IsBodyHtml = false;
            message.Body = $"Hello.\n\nWe want to inform you that your appointment is on {appointment.Date} at {appointment.Time}, and that your doctor will be {appointment.Doctor} .See you soon! \n\nHospital team.";
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("prendjov.p21@gmail.com", "Ch1XbaLdPZwmnXxcwtrQ5A==");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtp.SendMailAsync(message);

            //MailAddress from = new MailAddress("prendjov.p21@gmail.com",
            //   "Pance " + (char)0xD8 + " Prendjov",
            //System.Text.Encoding.UTF8);

            //// Set destinations for the email message.
            //MailAddress to = new MailAddress(info.PatientEmail);

            // Specify the message content.
            //MailMessage message = new MailMessage(from, to);
            //message.Body = $"Hello.\n\nWe want to inform you that your appointment is on {info.Appointment.DateOfAppointment}, and that your doctor will be {info.Appointment.Doctor} .See you soon! \n\nHospital team.";

            // Include some non-ASCII characters in body and subject.
            //string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            //message.Body += Environment.NewLine + someArrows;
            //message.BodyEncoding = System.Text.Encoding.UTF8;
            //message.Subject = "Upcoming appointment reminder" + someArrows;
            //message.SubjectEncoding = System.Text.Encoding.UTF8;

            // The userState can be any object that allows your callback
            // method to identify this send operation.
            // For this example, the userToken is a string constant.
            //string userState = "test message1";
            //client.SendAsync(message, userState);

            message.Dispose();

            return true; //new
        }
    }
}