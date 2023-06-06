namespace CoreLibrary.Models.Api._Domain.Responses
{
	public class AuthResult
	{
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Result { get; set; }
        public List<string> Errors { get; set; }
    }
}

