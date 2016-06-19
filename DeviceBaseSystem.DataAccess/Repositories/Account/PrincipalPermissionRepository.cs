using DeviceBaseSystem.DataAccess.Interfaces.Account;
using Anatoli.Common.DataAccess.Repositories;
using Anatoli.DataAccess.Models.Identity;

namespace DeviceBaseSystem.DataAccess.Repositories.Account
{
    public class PrincipalPermissionRepository : BaseAnatoliRepository<PrincipalPermission>, IPrincipalPermissionRepository
    {
        #region Ctors
        public PrincipalPermissionRepository() : this(new AnatoliDbContext()) { }
        public PrincipalPermissionRepository(AnatoliDbContext context)
            : base(context)
        {
        }

        #endregion

        //notice: new custom methods could be added in here
    }   
}