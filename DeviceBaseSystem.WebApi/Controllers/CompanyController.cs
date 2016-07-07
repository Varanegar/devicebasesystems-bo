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

    [RoutePrefix("api/device/company")]
    public class CompanyController : AnatoliApiController
    {
        #region company
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("ldlst")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCompanies()
        {
            try
            {
                var result = await new CompanyDomain(OwnerInfo).GetAllAsync<CompanyViewModel>();
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
        public async Task<IHttpActionResult> GetById([FromBody]CompanyRequestModel data)
        {
            try
            {
                var result = await new CompanyDomain(OwnerInfo).GetAllAsync<CompanyViewModel>(x => x.Id == data.uniqueId); 
                //await new CompanyDomain(OwnerInfo).GetByIdAsync<CompanyViewModel>(data.uniqueId);
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
        public async Task<IHttpActionResult> SaveCompanys([FromBody]CompanyRequestModel data)
        {
            try
            {
                var domain = new CompanyDomain(OwnerInfo);
                await domain.PublishAsync(AutoMapper.Mapper.Map<Company>(data.companyData));

                return Ok(data.companyData);

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
        public async Task<IHttpActionResult> DeleteCompanys([FromBody]CompanyRequestModel data)
        {
            try
            {
                var domain = new CompanyDomain(OwnerInfo);
                await domain.Delete(new List<Company>() { AutoMapper.Mapper.Map<Company>(data.companyData) });

                return Ok(data.companyData);

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
