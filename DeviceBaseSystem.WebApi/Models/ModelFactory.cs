using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.User;
using DeviceBaseSystem.WebApi.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace Anatoli.Cloud.WebApi.Models
{
    public class ModelFactory
    {

        private UrlHelper _UrlHelper;
        private ApplicationUserManager _AppUserManager;

        public ModelFactory(HttpRequestMessage request, ApplicationUserManager appUserManager)
        {
            _UrlHelper = new UrlHelper(request);
            _AppUserManager = appUserManager;
        }

        public UserReturnModel Create(User appUser)
        {
            return new UserReturnModel
            {
                Url = _UrlHelper.Link("GetUserById", new { id = appUser.Id }),
                Id = appUser.Id,
                UserName = appUser.UserNameStr,
                FullName = appUser.FullName,
                Email = appUser.Email,
                //EmailConfirmed = appUser.EmailConfirmed,
                CreateDate = appUser.CreatedDate,
                Mobile = appUser.PhoneNumber,
                //Roles = _AppUserManager.GetRolesAsync(appUser.Id).Result,
                //Claims = _AppUserManager.GetClaimsAsync(appUser.Id).Result
            };

        }

        public RoleReturnModel Create(IdentityRole appRole)
        {

            return new RoleReturnModel
           {
               Url = _UrlHelper.Link("GetRoleById", new { id = appRole.Id }),
               Id = appRole.Id,
               Name = appRole.Name
           };

        }
    }



    public class RoleReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}