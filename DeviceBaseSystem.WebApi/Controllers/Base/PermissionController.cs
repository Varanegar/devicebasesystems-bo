using Anatoli.Business.Domain.Permissions;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.AuthorizationModels;
using DeviceBaseSystem.WebApi.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers.Base
{
    [RoutePrefix("api/permissions")]
    public class PermissionController : AnatoliApiController
    {
        //[Authorize(Roles = "Admin")]
        [Route("save"), HttpPost]
        public async Task<IHttpActionResult> SavePermissions([FromBody]PermissionSaveViewModel permission)
        {
            try
            {
                var domain = new PermissionDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                Permission dbPermission = null;

                if (permission.PermissionId == Guid.Empty)
                {
                    // new
                    dbPermission = await domain.MainRepository.FindAsync(p =>
                        p.ApplicationModuleResourceId == permission.ResourceId &&
                        p.PermissionActionId == permission.ActionId);

                    if (dbPermission == null)
                    {
                        dbPermission = new Permission()
                        {
                            ApplicationModuleResourceId = permission.ResourceId,
                            PermissionActionId = permission.ActionId,
                            Name = permission.PermissionName,
                            Id = Guid.NewGuid()
                        };
                        domain.MainRepository.Add(dbPermission);
                    }
                    else
                        // Resource + Action already have a permission.
                        return Conflict();
                }
                else
                {
                    // update
                    dbPermission = await domain.MainRepository.FindAsync(p => p.Id == permission.PermissionId);
                    dbPermission.PermissionActionId = permission.ActionId;
                    dbPermission.ApplicationModuleResourceId = permission.ResourceId;
                    dbPermission.Name = permission.PermissionName;
                    domain.MainRepository.Update(dbPermission);
                }

                await domain.MainRepository.SaveChangesAsync();
                return Ok(permission);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("remove/{permissionId}"), HttpPost]
        public async Task<IHttpActionResult> RemovePermission([FromUri]Guid permissionId)
        {
            try
            {
                var domain = new PermissionDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var dbPermission = await domain.MainRepository.FindAsync(p => p.Id == permissionId);
                if (dbPermission == null)
                    return BadRequest("آیتم مورد نظر پیدا نشد.");

                domain.MainRepository.Delete(dbPermission);
                domain.MainRepository.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("addToCatalog/{catalogId}/{permissionId}"), HttpPost]
        public async Task<IHttpActionResult> AddToCatalog(Guid catalogId, Guid permissionId)
        {
            try
            {
                var domain = new PermissionDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                domain.MainRepository.AddToCatalog(catalogId, permissionId);
                await domain.MainRepository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("removeFromCatalog/{catalogId}/{permissionId}"), HttpPost]
        public async Task<IHttpActionResult> RemoveFromCatalog(Guid catalogId, Guid permissionId)
        {
            try
            {
                var domain = new PermissionDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                domain.MainRepository.RemoveFromCatalog(catalogId, permissionId);
                await domain.MainRepository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
