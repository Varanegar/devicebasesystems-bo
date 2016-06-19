using DeviceBaseSystem.DataAccess.Interfaces.Account;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System;
using Anatoli.Common.DataAccess.Repositories;
using Anatoli.DataAccess.Models.Identity;

namespace DeviceBaseSystem.DataAccess.Repositories.Account
{
    public class PermissionRepository : BaseAnatoliRepository<Permission>, IPermissionRepository
    {
        #region Ctors
        public PermissionRepository() : this(new AnatoliDbContext()) { }
        public PermissionRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion

        public IEnumerable<Permission> GetPermissionsWithDetails()
        {
            return ((AnatoliDbContext)DbContext).Permissions
                .Include(p => p.ApplicationModuleResource.ApplicationModule.Application)
                .Include(p => p.PermissionAction)
                .AsNoTracking();
        }

        public IEnumerable<Permission> GetPermissionsWithDetails(Guid permissionCatalogId)
        {
            return ((AnatoliDbContext)DbContext).Permissions
                .Where(p => p.PermissionCatalogPermissions.Any(c => c.PermissionCatalog.Id == permissionCatalogId))
                .Include(p => p.ApplicationModuleResource.ApplicationModule.Application)
                .Include(p => p.PermissionAction)
                .AsNoTracking();
        }

        public IEnumerable<PermissionAction> GetAllPermissionActions()
        {
            return ((AnatoliDbContext)DbContext).PermissionActions.AsNoTracking().ToList();
        }

        public void AddToCatalog(Guid catalogId, Guid permissionId)
        {
            var catalog = ((AnatoliDbContext)DbContext).PermissionCatalogs.Include(c => c.PermissionCatalogPermissions).FirstOrDefault(c => c.Id == catalogId);
            var permisson = ((AnatoliDbContext)DbContext).Permissions.FirstOrDefault(p => p.Id == permissionId);

            if (!catalog.PermissionCatalogPermissions.Any(p => p.Permission.Id == permissionId))
            {
                catalog.PermissionCatalogPermissions.Add(new PermissionCatalogPermission()
                {
                    Id = Guid.NewGuid(),
                    PermissionCatalog = catalog,
                    Permission = permisson
                });
            }
        }

        public void RemoveFromCatalog(Guid catalogId, Guid permissionId)
        {
            var catalog = ((AnatoliDbContext)DbContext).PermissionCatalogs.Include(c => c.PermissionCatalogPermissions).FirstOrDefault(c => c.Id == catalogId);
            var permisson = ((AnatoliDbContext)DbContext).Permissions.FirstOrDefault(p => p.Id == permissionId);
            var permissionCatalogPermission = catalog.PermissionCatalogPermissions.FirstOrDefault(p =>
                p.PermissionCatalog.Id == catalogId && p.Permission.Id == permissionId);
            if (permissionCatalogPermission != null)
            {
                catalog.PermissionCatalogPermissions.Remove(permissionCatalogPermission);
                permissionCatalogPermission.Permission = null;
                permissionCatalogPermission.PermissionCatalog = null;
                DbContext.Entry<PermissionCatalogPermission>(permissionCatalogPermission).State = EntityState.Deleted;
            }
        }
    }
}