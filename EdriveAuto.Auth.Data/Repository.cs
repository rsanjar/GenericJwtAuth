using EdriveAuto.GenericRepository;

namespace EDriveAuto.Auth.Data
{
    public class Repository<TEntity> : GenericRepository<TEntity>, IRepository<TEntity> where TEntity : class, IBaseRepositoryModel, new()
    {
	    #region ctor

        public Repository(EDriveAutoAuthDataContext context) : base(context)
        {
            Context = context;
        }

        #endregion

        public EDriveAutoAuthDataContext Context { get; }
    }
}
