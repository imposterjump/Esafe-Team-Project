using Esafe_Team_Project.Configurations;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace Esafe_Team_Project.Services
{
    public interface IEmailClient
    {
        void Send(string to, string subject, string html, string from = null);
        void SendWithPdf(string to, string subject, string html, byte[] pdfFile, string mediaType, string MediaSubtype, string fileName, string from = null);
        public Task SendAsync(string to, string subject, string html, string from = null);
        public Task SendWithPdfAsync(string to, string subject, string html, byte[] pdfFile, string mediaType, string mediaSubtype, string fileName, string from = null);

    }

    public class EmailClient : IEmailClient
    {
        private readonly MailerConfiguration configuration;
        private readonly ILogger<EmailClient> logger;

        public EmailClient(
            IOptions<MailerConfiguration> configurationOptions,
            ILogger<EmailClient> logger)
        {
            this.logger = logger;
            this.configuration = configurationOptions.Value;
        }

        public void Send(string to, string subject, string html, string from = null)
        {
            logger.LogInformation("Send Function Is Called From The EmailClient Service");
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(from ?? configuration.EmailFrom);
            email.From.Add(MailboxAddress.Parse(from ?? configuration.EmailFrom));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            //smtp.Connect(configuration.SmtpHost, configuration.SmtpPort, SecureSocketOptions.None);
            smtp.Connect(configuration.SmtpHost, configuration.SmtpPort, SecureSocketOptions.Auto);
            smtp.Authenticate(configuration.SmtpUser, configuration.SmtpPass);
            smtp.Send(email);
            smtp.Disconnect(true);
            logger.LogInformation("Email Is Sent From The EmailClient Service");
        }

        public async Task SendAsync(string to, string subject, string html, string from = null)
        {
            logger.LogInformation("Send Function Is Called From The EmailClient Service");
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(from ?? configuration.EmailFrom);
            email.From.Add(MailboxAddress.Parse(from ?? configuration.EmailFrom));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            //await smtp.ConnectAsync(configuration.SmtpHost, configuration.SmtpPort, SecureSocketOptions.None);
            smtp.Connect(configuration.SmtpHost, configuration.SmtpPort, SecureSocketOptions.Auto);
            smtp.Authenticate(configuration.SmtpUser, configuration.SmtpPass);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            logger.LogInformation("Email Is Sent From The EmailClient Service");
        }
        public void SendWithPdf(string to, string subject, string html, byte[] pdfFile, string mediaType, string mediaSubtype, string fileName, string from = null)
        {
            logger.LogInformation("Send Function Is Called From The EmailClient Service");
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(from ?? configuration.EmailFrom);
            email.From.Add(MailboxAddress.Parse(from ?? configuration.EmailFrom));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var multipart = new Multipart("mixed");


            var textPart = new TextPart(TextFormat.Html) { Text = html };
            multipart.Add(textPart);


            var attachment = new MimePart(mediaType, mediaSubtype)
            {
                Content = new MimeContent(new MemoryStream(pdfFile)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = fileName
            };
            multipart.Add(attachment);

            email.Body = multipart;

            //email.Body = new TextPart(TextFormat.Html) { Text = html };




            // send email
            using var smtp = new SmtpClient();
            //smtp.Connect(configuration.SmtpHost, configuration.SmtpPort, SecureSocketOptions.None);
            smtp.Connect(configuration.SmtpHost, configuration.SmtpPort, SecureSocketOptions.Auto);
            smtp.Authenticate(configuration.SmtpUser, configuration.SmtpPass);
            smtp.Send(email);
            smtp.Disconnect(true);
            logger.LogInformation("Email Is Sent From The EmailClient Service");
        }

        public async Task SendWithPdfAsync(string to, string subject, string html, byte[] pdfFile, string mediaType, string mediaSubtype, string fileName, string from = null)
        {
            logger.LogInformation("Send Function Is Called From The EmailClient Service");
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(from ?? configuration.EmailFrom);
            email.From.Add(MailboxAddress.Parse(from ?? configuration.EmailFrom));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var multipart = new Multipart("mixed");


            var textPart = new TextPart(TextFormat.Html) { Text = html };
            multipart.Add(textPart);


            var attachment = new MimePart(mediaType, mediaSubtype)
            {
                Content = new MimeContent(new MemoryStream(pdfFile)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = fileName
            };
            multipart.Add(attachment);

            email.Body = multipart;

            //email.Body = new TextPart(TextFormat.Html) { Text = html };




            // send email
            using var smtp = new SmtpClient();
            //await smtp.ConnectAsync(configuration.SmtpHost, configuration.SmtpPort, SecureSocketOptions.None);
            smtp.Connect(configuration.SmtpHost, configuration.SmtpPort, SecureSocketOptions.Auto);
            smtp.Authenticate(configuration.SmtpUser, configuration.SmtpPass);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            logger.LogInformation("Email Is Sent From The EmailClient Service");
        }

    }
}
