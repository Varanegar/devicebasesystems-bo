using DeviceBaseSystem.DataAccess;
using Anatoli.DataAccess.Models.Identity;
using DeviceBaseSystem.DataAccess.Repositories.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Business.Domain.Permissions
{
    public class PermissionDomain : RawBusinessDomain<PermissionRepository>
    {
        public PermissionDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey) :
            this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {
        }

        public PermissionDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext context)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey)
        {
            MainRepository = new PermissionRepository(context);
        }

        public IEnumerable<Permission> GetAllPermissions()
        {
            return MainRepository.GetPermissionsWithDetails();
        }

        public IEnumerable<Permission> GetAllPermissionsOfCatalog(Guid permissionCatalogId)
        {
            return MainRepository.GetPermissionsWithDetails(permissionCatalogId);
        }

        public IEnumerable<PermissionAction> GetAllPermissionActions()
        {
            return MainRepository.GetAllPermissionActions();
        }
    }
}
