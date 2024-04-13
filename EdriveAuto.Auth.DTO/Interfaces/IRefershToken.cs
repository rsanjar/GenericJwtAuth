using EdriveAuto.DTO;

namespace EDriveAuto.Auth.DTO.Interfaces
{
    public interface IRefreshToken : IBaseDTOModel
    {
        string Token { get; set; }
        int AccountID { get; set; }
        DateTime ExpireAt { get; set; }
    }
}