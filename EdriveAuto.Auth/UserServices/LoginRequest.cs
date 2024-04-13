using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EDriveAuto.Auth.DTO;

namespace EDriveAuto.Auth.UserServices
{
	public class LoginRequest
	{
		public LoginRequest()
		{
			UserName = string.Empty;
			Password = string.Empty;
			Role = UserRoles.Dealer; //default role
		}

		public LoginRequest(string userName, string password)
		{
			UserName = userName;
			Password = password;
			Role = UserRoles.Dealer; //default role
		}

		[Required]
		[JsonPropertyName("username")]
		public string UserName { get; set; }

		[Required]
		[JsonPropertyName("password")]
		public string Password { get; set; }

		[JsonPropertyName("role")]
		public string Role { get; set; }
	}
}