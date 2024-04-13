namespace EDriveAuto.Auth.DTO
{
    public partial class Account
    {
        public int ID { get; set; }
        public string MobilePhone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsPhoneVerified { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public Guid UniqueKey { get; set; }
        public int SmsActivationCode { get; set; }
        public DateTime? SmsActivationDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
        public DateTime? EmailVerificationDate { get; set; }
        public int AccountRoleID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}