using System.Text.Json.Serialization;

namespace EDriveAuto.Auth.JwtManager
{
	public class JwtAuthResult
	{
		public JwtAuthResult()
		{
			AccessToken = string.Empty;
			RefreshToken = new RefreshToken();
		}

		public JwtAuthResult(string accessToken, RefreshToken refreshToken)
		{
			AccessToken = accessToken;
			RefreshToken = refreshToken;
		}

		[JsonPropertyName("accessToken")]
		public string AccessToken { get; set; }

		[JsonPropertyName("refreshToken")]
		public RefreshToken RefreshToken { get; set; }
	}
}