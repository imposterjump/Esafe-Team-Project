namespace Esafe_Team_Project.Configurations
{
    public class MailerConfiguration
    {
        public string EmailFrom { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
        public string FrontEndURL { get; set; }
    }
}
