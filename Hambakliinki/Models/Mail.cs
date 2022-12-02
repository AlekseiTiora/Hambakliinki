using System.Net;
using System.Net.Mail;

namespace Hambakliinki.Models
{
    public class Mail
    {

        public async void SendEmailDefault( string Data, string email)
        {
            try
            {

                MailMessage message = new MailMessage();

                message.IsBodyHtml = true;
                message.From = new MailAddress("hambakliinik@hotmail.com", "Hambakliinik");
                message.To.Add(email);
                message.To.Add("hambakliinik@hotmail.com");
                message.Subject = "ASP.NET";
                message.Body = "<div style=\"\">Tere, Äitah! et broneeritud, helistame teile sobival ajal" + Data + ". Soovime teile parimat päeva</div>";

                var client = new SmtpClient("smtp.office365.com", 587)
                {
                    Credentials = new NetworkCredential("hambakliinik@hotmail.com", "ZubnoiVrach123!"),
                    EnableSsl = true
                };
                client.Send(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
