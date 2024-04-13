using EdriveAuto.DTO;

namespace EDriveAuto.Auth.DTO.Interfaces
{
    public interface IAccountRole : IBaseDTOModel
    {
        public string Name { get; set; }
    }
}