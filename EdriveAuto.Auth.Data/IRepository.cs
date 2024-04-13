using EdriveAuto.GenericRepository;

namespace EDriveAuto.Auth.Data
{
	public interface IRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IBaseRepositoryModel, new()
    {
	    EDriveAutoAuthDataContext Context { get; }
    }
}