using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Anatoli.ViewModels.DeviceBaseSystem;
using Anatoli.ViewModels.RequestModel.DeviceBaseSystem;
using DeviceBaseSystem.Business.Domain;
using DeviceBaseSystem.DataAccess.Models;
using DeviceBaseSystem.WebApi.Classes;

namespace DeviceBaseSystem.Controllers
{

    [RoutePrefix("api/device/companydev")]
    public class CompanyDeviceController : AnatoliApiController
    {
        #region company device
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("ldlst")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCompanyDevices([FromBody]CompanyDeviceRequestModel data)
        {
            try
            {
                var result = await new CompanyDeviceDomain(OwnerInfo).GetAllAsync<CompanyDeviceViewModel>(x => x.CompanyId == data.companyDeviceData.CompanyId);
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
        public async Task<IHttpActionResult> GetById([FromBody]CompanyDeviceRequestModel data)
        {
            try
            {
                var result = await new CompanyDeviceDomain(OwnerInfo).GetAllAsync<CompanyDeviceViewModel>(x => x.Id == data.uniqueId); 
                //await new CompanyDeviceDomain(OwnerInfo).GetByIdAsync<CompanyDeviceViewModel>(data.uniqueId);
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
        public async Task<IHttpActionResult> SaveCompanys([FromBody]CompanyDeviceRequestModel data)
        {
            try
            {
                var domain = new CompanyDeviceDomain(OwnerInfo);
                await domain.PublishAsync(AutoMapper.Mapper.Map<CompanyDevice>(data.companyDeviceData));

                return Ok(data.companyDeviceData);

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
        public async Task<IHttpActionResult> DeleteCompanys([FromBody]CompanyDeviceRequestModel data)
        {
            try
            {
                var domain = new CompanyDeviceDomain(OwnerInfo);
                await domain.Delete(new List<CompanyDevice>() { AutoMapper.Mapper.Map<CompanyDevice>(data.companyDeviceData) });

                return Ok(data.companyDeviceData);

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
