using Anatoli.DataAccess.Models.Identity;
using DeviceBaseSystem.DataAccess.Interfaces.Account;
using Anatoli.Common.DataAccess.Repositories;

namespace DeviceBaseSystem.DataAccess.Repositories.Account
{
    public class PrincipalPermissionCatalogRepository : BaseAnatoliRepository<PrincipalPermissionCatalog>, IPrincipalPermissionCatalogRepository
    {
        #region Ctors
        public PrincipalPermissionCatalogRepository() : this(new AnatoliDbContext()) { }
        public PrincipalPermissionCatalogRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion

        //notice: new custom methods could be added in here
    }
}
