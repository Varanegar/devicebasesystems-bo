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

    [RoutePrefix("api/device/brnd")]
    public class BrandController : AnatoliApiController
    {
        #region Loyalty Tier
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("ldlst")]
        [HttpPost]
        public async Task<IHttpActionResult> GetBrands()
        {
            try
            {
                var result = await new BrandDomain(OwnerInfo).GetAllAsync<BrandViewModel>();
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
        public async Task<IHttpActionResult> GetById([FromBody]BrandRequestModel data)
        {
            try
            {
                var result = await new BrandDomain(OwnerInfo).GetAllAsync<BrandViewModel>(x => x.Id == data.uniqueId); 
                //await new BrandDomain(OwnerInfo).GetByIdAsync<BrandViewModel>(data.uniqueId);
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
        public async Task<IHttpActionResult> SaveBrands([FromBody]BrandRequestModel data)
        {
            try
            {
                var domain = new BrandDomain(OwnerInfo);
                await domain.PublishAsync(AutoMapper.Mapper.Map<IEnumerable<Brand>>(data.brandData).ToList());

                return Ok(data.brandData);

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
        public async Task<IHttpActionResult> DeleteBrands([FromBody]BrandRequestModel data)
        {
            try
            {
                var domain = new BrandDomain(OwnerInfo);
                await domain.DeleteBrands(AutoMapper.Mapper.Map<IEnumerable<Brand>>(data.brandData).ToList());

                return Ok(data.brandData);

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
