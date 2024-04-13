using System.ComponentModel.DataAnnotations;

namespace EDriveAuto.Auth.Data.Models
{
    public partial class AccountRole
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public virtual List<Account> Accounts { get; set; } = new();
    }
}