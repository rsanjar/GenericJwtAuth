using EdriveAuto.GenericRepository;

namespace EDriveAuto.Auth.Data.Models.Interfaces
{
    public interface IRefreshToken : IBaseRepositoryModel
    {
        string Token { get; set; }

        int AccountID { get; set; }
    }
}