namespace EventManager.Web.Models
{
    public class TokenModel
    {
        public TokenModel(string token)
        {
            Token = token;    
        }

        public string? Token { get; set; }
    }
}
