using System;
using System.Linq;
using DeviceBaseSystem.DataAccess;
using System.Threading.Tasks;
using System.Collections.Generic;
using DeviceBaseSystem.DataAccess.Repositories;
using Anatoli.ViewModels.AuthorizationModels;
using DeviceBaseSystem.DataAccess.Repositories.Account;
using NLog;
using Anatoli.Common.DataAccess.Interfaces;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.Business.Domain.Authorization
{

    public class AuthorizationDomain 
    {
        #region Properties
        protected static readonly Logger Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());
        public IBaseRepository<Principal> PrincipalRepository { get; set; }
        public IBaseRepository<Permission> PermissionRepository { get; set; }
        public IBaseRepository<PrincipalPermission> PrincipalPermissionRepository { get; set; }

        public IBaseRepository<PermissionCatalog> PermissionCatalogRepository { get; set; }
        public IBaseRepository<PrincipalPermissionCatalog> PrincipalPermissionCatalogRepository { get; set; }

        public AnatoliDbContext DBContext { get; set; }
        public IBaseRepository<PrincipalPermission> MainRepository { get; set; }
        public Guid ApplicationOwnerKey { get; protected set; }
        public Guid DataOwnerKey { get; protected set; }
        public Guid DataOwnerCenterKey { get; protected set; }
        #endregion

        #region Ctors
        public AuthorizationDomain(Guid applicationKey)
            : this(applicationKey, applicationKey, applicationKey)
        { }
        public AuthorizationDomain(Guid applicationKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        { }
        public AuthorizationDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
        {
            ApplicationOwnerKey = applicationOwnerKey; DataOwnerKey = dataOwnerKey; DataOwnerCenterKey = dataOwnerCenterKey;

            PermissionRepository = new PermissionRepository(dbc);
            PermissionCatalogRepository = new PermissionCatalogRepository(dbc);
            PrincipalPermissionCatalogRepository = new PrincipalPermissionCatalogRepository(dbc);
            MainRepository = new PrincipalPermissionRepository(dbc);
            PrincipalRepository = new PrincipalRepository(dbc);

            PrincipalPermissionRepository = new PrincipalPermissionRepository(dbc);

            DBContext = dbc;
        }
        #endregion

        #region Methods
        public List<PrincipalPermission> GetPermissionsForPrincipal(string principalId, string resource, string action)
        {
            //Todo: get all other related principal such as roles and groups
            var model = MainRepository.GetQuery().Where(p => p.PrincipalId == Guid.Parse(principalId) &&
                                                        p.Permission.ApplicationModuleResource.Name == resource &&
                                                        p.Permission.PermissionAction.Name == action)
                                                 .ToList();
            return model;
        }
        public async Task<ICollection<PrincipalPermissionViewModel>> GetPermissionsForPrincipal(string principalId)
        {
            //Todo: get all other related principal such as roles and groups

            var pid = Guid.Parse(principalId);
            var model = await MainRepository.GetFromCachedAsync(pp => pp.PrincipalId == pid, s => new PrincipalPermissionViewModel
            {
                PrincipalId = principalId,
                Resource = s.Permission.ApplicationModuleResource.Name,
                Action = s.Permission.PermissionAction.Name,
                Grant = (s.Grant == 1) ? true : false,
                PermissionId = s.Permission_Id
            });

            return model.ToList();
        }
        public async Task SavePermissions(List<PrincipalPermission> pp, string principalId)
        {
            try
            {
                await MainRepository.DeleteBatchAsync(p => p.PrincipalId == Guid.Parse(principalId));

                foreach (var item in pp)
                    await MainRepository.AddAsync(item);

                await MainRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        public async Task<ICollection<PermissionViewModel>> GetAllPermissions()
        {
            var model = await PermissionRepository.GetFromCachedAsync(p => p.Id == ApplicationOwnerKey, s => new PermissionViewModel
            {
                Id = s.Id,
                Resource = s.ApplicationModuleResource.Name,
                Action = s.PermissionAction.Name,
                Title = s.Name
            });
            return model.ToList();
        }
        public async Task<ICollection<PermissionViewModel>> GetAllPermissionsOfCatalog(string catalogId)
        {
            var model = await PermissionRepository.GetFromCachedAsync(
                p => p.Id == ApplicationOwnerKey && 
                p.PermissionCatalogPermissions.Any(q => q.PermissionCatalog.Id == Guid.Parse(catalogId)), 
                s => new PermissionViewModel
            {
                Id = s.Id,
                Resource = s.ApplicationModuleResource.Name,
                Action = s.PermissionAction.Name,
                Title = s.Name
            });
            return model.ToList();
        }

        public async Task<ICollection<PermissionCatalogViewModel>> GetAllPermissionCatalogs()
        {
            // p.Id == ApplicationOwnerKey
            var model = await PermissionCatalogRepository.GetFromCachedAsync(p => true, s => new PermissionCatalogViewModel
            {
                Id = s.Id,
                Title = s.Name,
                Parent = s.PermissionCatalougeParentId.HasValue ? s.PermissionCatalougeParentId.Value.ToString() : ""
            });
            return model.ToList();
        }
        public async Task<ICollection<PrincipalPermissionCatalogViewModel>> GetPermissionCatalogsForPrincipal(string principalId)
        {
            var pid = Guid.Parse(principalId);
            var model = await PrincipalPermissionCatalogRepository.GetFromCachedAsync(pp => pp.PrincipalId == pid, s => new PrincipalPermissionCatalogViewModel
            {
                Id = s.Id,
                PrincipalId = principalId,
                Grant = (s.Grant == 1) ? true : false,
                PermissionCatalogId = s.PermissionCatalog_Id
            });

            return model.ToList();
        }
        public async Task SavePermissionCatalogs(List<PrincipalPermissionCatalog> principalPermissionCatalogs, Guid principalId)
        {
            try
            {
                await PrincipalPermissionCatalogRepository.DeleteBatchAsync(p => p.PrincipalId == principalId);

                await PrincipalPermissionRepository.DeleteBatchAsync(p => p.PrincipalId == principalId);

                foreach (var item in principalPermissionCatalogs)
                {
                    await PrincipalPermissionCatalogRepository.AddAsync(item);

                    var permissionCatalog = await PermissionCatalogRepository.GetByIdAsync(item.PermissionCatalog_Id);

                    permissionCatalog.PermissionCatalogPermissions.ToList().ForEach(itm =>
                    {
                        PrincipalPermissionRepository.AddAsync(new PrincipalPermission
                        {
                            Id = Guid.NewGuid(),
                            Grant = item.Grant,
                            Permission_Id = itm.Id,
                            PrincipalId = principalId
                        });
                    });
                }

                await PrincipalPermissionCatalogRepository.SaveChangesAsync();

                await PrincipalPermissionRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #endregion
    }
}
