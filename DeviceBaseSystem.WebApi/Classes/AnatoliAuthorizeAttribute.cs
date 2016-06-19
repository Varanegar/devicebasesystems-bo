using System;
using System.Net;
using System.Web;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using DeviceBaseSystem.WebApi.Infrastructure;

namespace DeviceBaseSystem.WebApi.Classes
{
    public class AnatoliAuthorizeAttribute : AuthorizeAttribute
    {
        #region Properties
        private string _responseReason = "";
        public bool ByPassAuthorization { get; set; }

        public Guid OwnerKey
        {
            get
            {
                return Guid.Parse(HttpContext.Current.Request.Headers["OwnerKey"]);
            }
        }
        public Guid DataOwnerKey
        {
            get
            {
                if (HttpContext.Current.Request.Headers["DataOwnerKey"] == null)
                    return OwnerKey;
                else
                    return Guid.Parse(HttpContext.Current.Request.Headers["DataOwnerKey"]);
            }
        }
        public Guid DataOwnerCenterKey
        {
            get
            {
                if (HttpContext.Current.Request.Headers["DataOwnerCenterKey"] == null)
                    return DataOwnerKey;
                else
                    return Guid.Parse(HttpContext.Current.Request.Headers["DataOwnerCenterKey"]);
            }
        }
        public bool HasOwnerKey
        {
            get
            {
                return OwnerKey != null ? true : false;
            }
        }
        public string Resource { get; set; }
        public string Action { get; set; }
        #endregion

        private string _currentUserId;
        public string CurrentUserId
        {
            get
            {
                if (string.IsNullOrEmpty(_currentUserId))
                    _currentUserId = GetUserId();

                return _currentUserId;
            }
        }

        #region Methods
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            if (!string.IsNullOrEmpty(_responseReason))
                actionContext.Response.ReasonPhrase = _responseReason;
        }

        private IEnumerable<AnatoliAuthorizeAttribute> GetApiAuthorizeAttributes(HttpActionDescriptor descriptor)
        {
            return descriptor.GetCustomAttributes<AnatoliAuthorizeAttribute>(true)
                             .Concat(descriptor.ControllerDescriptor.GetCustomAttributes<AnatoliAuthorizeAttribute>(true));
        }

        private bool IsApiPageRequested(HttpActionContext actionContext)
        {
            var apiAttributes = GetApiAuthorizeAttributes(actionContext.ActionDescriptor);
            if (apiAttributes != null && apiAttributes.Any())
                return true;
            return false;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                if (IsApiPageRequested(actionContext))
                    if (!HasOwnerKey)
                    {
                        HandleUnauthorizedRequest(actionContext);

                        _responseReason = "Application key required.";
                    }
                    else
                        base.OnAuthorization(actionContext);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private bool HasWebApiAccess()
        {
            if (string.IsNullOrEmpty(Resource) && string.IsNullOrEmpty(Action))
                return true;

            if (string.IsNullOrEmpty(CurrentUserId))
                return false;

            //var permissions = new AuthorizationDomain(Guid.Parse(OwnerKey.ToString())).GetPermissionsForPrincipal(user.GetAnatoliUserId(), Resource, Action);

            //if (permissions == null || permissions.Count == 0 || permissions.Any(a => a.Grant == -1))
            //    return false;

            //check resource action from db for this Principal.

            return true;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            //Todo: check request ownerKey with claim ownerKey here

            //logic for check whether we have an attribute with ByPassAuthorization = true e.g [ByPassAuthorization(true)], if so then just return true 
            if (ByPassAuthorization || GetApiAuthorizeAttributes(actionContext.ActionDescriptor).Any(x => x.ByPassAuthorization))
                return true;

            //checking against our custom table goes here
            if (!HasWebApiAccess())
            {
                HandleUnauthorizedRequest(actionContext);

                _responseReason = "Access Denied";

                return false;
            }

            return base.IsAuthorized(actionContext);
        }

        private string GetUserId()
        {
            if (HttpContext.Current.User == null)
                return string.Empty;

            var uid = HttpContext.Current.User.Identity.GetUserId();

            if (!string.IsNullOrEmpty(uid))
                return uid;

            var email = ClaimsPrincipal.Current.Claims.Where(c => c.Type == "Email").Select(s => s.Value).FirstOrDefault();

            var AppUserManager = HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var user = AppUserManager.FindByNameOrEmailOrPhone(email, OwnerKey, DataOwnerKey);

            if (user == null)
                return string.Empty;

            return user.Id;
        }
        #endregion
    }

    //public class RequireHttpsAttribute : AuthorizeAttribute
    //{
    //    public override void OnAuthorization(HttpActionContext actionContext)
    //    {
    //        if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
    //        {
    //            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
    //            {
    //                ReasonPhrase = "HTTPS Required"
    //            };
    //        }
    //        else
    //        {
    //            base.OnAuthorization(actionContext);
    //        }
    //    }
    //}
}