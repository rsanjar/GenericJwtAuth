using EDriveAuto.Auth.Data;
using EDriveAuto.Auth.DTO;
using EDriveAuto.Auth.Service.Interfaces;
using EdriveAuto.GenericService;
using Microsoft.EntityFrameworkCore;

namespace EDriveAuto.Auth.Service
{
    public class AuthenticatableService : BaseService<Account, Data.Models.Account>, IAuthenticatable
    {
        #region ctor

        private readonly IRepository<Data.Models.Account> _repository;

        public AuthenticatableService(IRepository<Data.Models.Account> repository) : base(repository)
        {
            _repository = repository;
        }

        #endregion
        
        public async Task<Account?> GetUserByUniqueKey(Guid uniqueKey)
        {
            var result  =  await FirstOrDefaultAsync(_repository.Context.Accounts.Where(p => p.UniqueKey == uniqueKey));
            
            return result;
        }

        public async Task<bool> UpdateLastLoginDate(Guid uniqueKey)
        {
            var account = await _repository.Context.Accounts.FirstOrDefaultAsync(p => p.UniqueKey == uniqueKey);

            if (account == null)
            {
                return false;
            }

            account.LastLoginDate = DateTime.Now;

            return await _repository.Context.SaveChangesAsync() > 0;
        }
    }
}