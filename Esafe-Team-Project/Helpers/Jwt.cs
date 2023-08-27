namespace Esafe_Team_Project.Helpers
{
    public class Jwt
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int ExpireTime { get; set; }


    }
}
