using System.Text.Json.Serialization;

namespace EDriveAuto.Auth.UserServices
{
	public class RefreshTokenRequest
	{
		[JsonPropertyName("refreshToken")]
		public string RefreshToken { get; set; } = string.Empty;
	}
}