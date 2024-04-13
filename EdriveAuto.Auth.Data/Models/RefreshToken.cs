using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDriveAuto.Auth.Data.Models
{
    public partial class RefreshToken
    {
	    [Required] public string Token { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Accounts))]
        public int AccountID { get; set; }

        [Required]
        public DateTime ExpireAt { get; set; }


        public virtual Account Accounts { get; set; } = null!;
    }
}