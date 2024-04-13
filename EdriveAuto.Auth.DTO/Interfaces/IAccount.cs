using System.ComponentModel.DataAnnotations;
using EdriveAuto.DTO;
using Newtonsoft.Json;

namespace EDriveAuto.Auth.DTO.Interfaces
{
	public interface IAccount : IBaseDTOModel
	{
		[Required]
		[DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone", Prompt = "Phone")]
		string MobilePhone { get; set; }

		[Required]
		[JsonIgnore]
		[DataType(DataType.Password)]
		[System.Text.Json.Serialization.JsonIgnore]
        [Display(Name = "Password", Prompt = "Password")]
		string Password { get; set; }

        [Display(Name = "Is Active", Prompt = "Is Active")]
        bool IsActive { get; set; }
		
        bool IsPhoneVerified { get; set; }
		
        DateTime? LastLoginDate { get; set; }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        Guid UniqueKey { get; set; }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        int SmsActivationCode { get; set; }
		
        DateTime? SmsActivationDate { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Email")]
        string Email { get; set; }
		
        bool IsEmailVerified { get; set; }
		
        DateTime? EmailVerificationDate { get; set; }

        int AccountRoleID { get; set; }
	}
}