using System.Text.Json.Serialization;

namespace EDriveAuto.Auth.JwtManager
{
	public class JwtTokenConfig
	{
		public JwtTokenConfig()
		{
			Secret = string.Empty;
			Issuer = string.Empty;
			Audience = string.Empty;
			AccessTokenExpiration = 30;
			RefreshTokenExpiration = 200;
		}

		public JwtTokenConfig(string secret, string issuer, string audience, int accessTokenExpiration, int refreshTokenExpiration)
		{
			Secret = secret;
			Issuer = issuer;
			Audience = audience;
			AccessTokenExpiration = accessTokenExpiration;
			RefreshTokenExpiration = refreshTokenExpiration;
		}

		[JsonPropertyName("secret")]
		public string Secret { get; set; }

		[JsonPropertyName("issuer")]
		public string Issuer { get; set; }

		[JsonPropertyName("audience")]
		public string Audience { get; set; }

		[JsonPropertyName("accessTokenExpiration")]
		public int AccessTokenExpiration { get; set; }

		[JsonPropertyName("refreshTokenExpiration")]
		public int RefreshTokenExpiration { get; set; }
	}
}