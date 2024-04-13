namespace EDriveAuto.Auth.DTO
{
    public partial class AccountRole
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}