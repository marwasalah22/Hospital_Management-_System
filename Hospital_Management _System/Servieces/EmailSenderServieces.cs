namespace Hospital_Management__System.Servieces
{
    public class EmailSenderServieces : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var websiteEmail = "HospitalManagement111@outlook.com";
            var websitePassword = "Hospital111";

            var msg = new MailMessage();
            msg.From = new MailAddress(websiteEmail);
            msg.Subject = subject;
            msg.To.Add(email);
            msg.Body = $"<html><body>" +
                $"<p>{htmlMessage}</p>" +
                $"</body></html>";
            msg.IsBodyHtml = true;


            var smtpClient = new SmtpClient("smtp.office365.com", 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(websiteEmail, websitePassword)
            };

            try
            {
                smtpClient.Send(msg);

            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"SMTP Errors : {ex.Message}");
            }
        }
    }
}
