using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Anatoli.Common.DataAccess.Models;
using System.Threading.Tasks;
using Anatoli.ViewModels.DeviceBaseSystem;
using Anatoli.ViewModels.RequestModel.DeviceBaseSystem;
using DeviceBaseSystem.Business.Domain;
using DeviceBaseSystem.DataAccess.Models;
using DeviceBaseSystem.WebApi.Classes;

namespace DeviceBaseSystem.Controllers
{
    [RoutePrefix("api/device/devmdl")]
    public class DeviceModelController : AnatoliApiController
    {
        #region Loyalty Tier
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("ldlst")]
        [HttpPost]
        public async Task<IHttpActionResult> GetDeviceModels()
        {
            try
            {
                var result = await new DeviceModelDomain(OwnerInfo).GetAllAsync<DeviceModelViewModel>();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Web API Call Error");
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("byid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetById([FromBody]DeviceModelRequestModel data)
        {
            try
            {
                var result = await new DeviceModelDomain(OwnerInfo).GetAllAsync<DeviceModelViewModel>(x => x.Id == data.uniqueId); 
                //await new DeviceModelDomain(OwnerInfo).GetByIdAsync<DeviceModelViewModel>(data.uniqueId);
                if (result.Any())
                    return Ok(result[0]);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error(ex, "Web API Call Error");
                return GetErrorResult(ex);
            }
        }

       

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveDeviceModels([FromBody]DeviceModelRequestModel data)
        {
            try
            {
                var domain = new DeviceModelDomain(OwnerInfo);
                await domain.PublishAsync(AutoMapper.Mapper.Map<DeviceModel>(data.deviceModelData));

                return Ok(data.deviceModelData);

            }
            catch (Exception ex)
            {
                log.Error(ex, "Web API Call Error");
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("rmv")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteDeviceModels([FromBody]DeviceModelRequestModel data)
        {
            try
            {
                var domain = new DeviceModelDomain(OwnerInfo);
                await domain.Delete(new List<DeviceModel>(){AutoMapper.Mapper.Map<DeviceModel>(data.deviceModelData)});

                return Ok(data.deviceModelData);

            }
            catch (Exception ex)
            {
                log.Error(ex, "Web API Call Error");
                return GetErrorResult(ex);
            }
        }
        #endregion

    }
}
