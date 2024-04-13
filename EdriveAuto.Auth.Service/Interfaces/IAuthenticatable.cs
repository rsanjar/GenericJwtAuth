using EDriveAuto.Auth.DTO;
using EdriveAuto.GenericService.Interfaces;

namespace EDriveAuto.Auth.Service.Interfaces
{
    public interface IAuthenticatable : IBaseService<Account>
    {
        Task<Account?> GetUserByUniqueKey(Guid uniqueKey);

        Task<bool> UpdateLastLoginDate(Guid uniqueKey);
    }
}
