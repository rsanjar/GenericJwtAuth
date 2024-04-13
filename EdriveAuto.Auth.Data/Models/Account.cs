using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDriveAuto.Auth.Data.Models
{
    public partial class Account
    {
        [Required]
        [StringLength(40)]
        public string MobilePhone { get; set; } = null!;
        
        [Required]
        [StringLength(120)]
        public string Password { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; }
        
        [Required]
        public bool IsPhoneVerified { get; set; }
        
        public DateTime? LastLoginDate { get; set; }
 
        [Required]
        public Guid UniqueKey { get; set; }
        
        [Required]
        [MaxLength(5)]
        [MinLength(5)]
        public int? SmsActivationCode { get; set; }
        
        public DateTime? SmsActivationDate { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Email { get; set; } = null!;
        
        [Required]
        public bool IsEmailVerified { get; set; }
        
        public DateTime? EmailVerificationDate { get; set; }

        [Required]
        [ForeignKey(nameof(Role))]
        public int AccountRoleID { get; set; }


        public virtual AccountRole Role { get; set; } = null!;

        public virtual List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}