using Anatoli.Business.Domain.Permissions;
using Anatoli.ViewModels.AuthorizationModels;
using DeviceBaseSystem.WebApi.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers.Base
{
    [RoutePrefix("api/action")]
    public class ActionController : AnatoliApiController
    {
        [Authorize(Roles = "Admin")]
        [Route("list"), HttpGet]
        public IHttpActionResult GetActions()
        {
            try
            {
                var domain = new PermissionDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var model = domain.GetAllPermissionActions().Select(a => new PermissionActionViewModel()
                {
                    ActionId = a.Id,
                    ActionName = a.Name
                }).ToList();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[Authorize(Roles = "Admin")]
        //[Route("list"), HttpPost]
        //public IHttpActionResult Save(PermissionActionViewModel aciton)
        //{
        //    try
        //    {


        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        InternalServerError(ex);
        //    }
        //}
    }
}
