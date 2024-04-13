namespace EDriveAuto.Auth.DTO
{
    public partial class RefreshToken
    {
        public int ID { get; set; }
        public string Token { get; set; } = string.Empty;
        public int AccountID { get; set; }
        public DateTime ExpireAt { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}