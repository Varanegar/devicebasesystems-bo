using Anatoli.Common.DataAccess.Repositories;
using Anatoli.DataAccess.Models.Identity;

namespace DeviceBaseSystem.DataAccess.Repositories.Account
{
    public class PermissionCatalogRepository : BaseAnatoliRepository<PermissionCatalog>, IPermissionCatalogRepository
    {
        #region Ctors
        public PermissionCatalogRepository() : this(new AnatoliDbContext()) { }
        public PermissionCatalogRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion

        //notice: new custom methods could be added in here
    }
}
