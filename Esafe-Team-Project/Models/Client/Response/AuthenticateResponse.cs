namespace Esafe_Team_Project.Models.Client.Response
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
        public string Email { get; set; }
    }
}
