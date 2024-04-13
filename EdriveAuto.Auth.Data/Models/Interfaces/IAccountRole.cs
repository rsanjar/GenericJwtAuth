using EdriveAuto.GenericRepository;

namespace EDriveAuto.Auth.Data.Models.Interfaces
{
    public interface IAccountRole : IBaseRepositoryModel
    {
        string Name { get; set; }
    }
}