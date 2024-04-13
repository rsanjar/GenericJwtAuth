using System.Text.Json.Serialization;

namespace EDriveAuto.Auth.JwtManager
{
	public class RefreshToken
	{
		public RefreshToken()
		{
			UserName = string.Empty;
			TokenString = string.Empty;
			ExpireAt = DateTime.UtcNow.AddDays(30);
		}

		public RefreshToken(string userName, string tokenString, DateTime expireAt)
		{
			UserName = userName;
			TokenString = tokenString;
			ExpireAt = expireAt;
		}

		// can be used for usage tracking
		// can optionally include other metadata, such as user agent, ip address, device name, and so on
		[JsonPropertyName("username")]
		public string UserName { get; set; }    
		
		[JsonPropertyName("tokenString")]
		public string TokenString { get; set; }

		[JsonPropertyName("expireAt")]
		public DateTime ExpireAt { get; set; }
	}
}