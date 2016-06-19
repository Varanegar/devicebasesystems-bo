using Anatoli.Common.DataAccess.Repositories;
using DeviceBaseSystem.DataAccess.Interfaces;
using Anatoli.DataAccess.Models.Identity;

namespace DeviceBaseSystem.DataAccess.Repositories
{
    public class UserRepository : BaseAnatoliRepository<User>, IUserRepository
    {
        #region Ctors
        public UserRepository() : this(new AnatoliDbContext()) { }
        public UserRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
        
        //notice: new custom methods could be added in here
    }    
}
