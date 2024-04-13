using System.Text.Json.Serialization;

namespace EDriveAuto.Auth.UserServices
{
	public class ImpersonationRequest
	{
		[JsonPropertyName("username")]
		public string UserName { get; set; } = string.Empty;
	}
}