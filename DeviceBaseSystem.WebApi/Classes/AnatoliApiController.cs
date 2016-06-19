using System;
using System.Web;
using System.Globalization;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Linq;
using Anatoli.Common.DataAccess.Models;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.Cloud.WebApi.Controllers;

namespace DeviceBaseSystem.WebApi.Classes
{
    public abstract class AnatoliApiController : BaseApiController
    {
        public bool GetRemovedData
        {
            get
            {
                string data = HttpContext.Current.Request.Headers["GetRemovedData"];
                return (data == null) ? true : bool.Parse(data);
            }
        }

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

        public OwnerInfo OwnerInfo
        {
            get
            {
                return new OwnerInfo
                {
                    ApplicationOwnerKey = OwnerKey,
                    DataOwnerCenterKey = DataOwnerCenterKey,
                    DataOwnerKey = DataOwnerKey
                };
            }
        }

        public DateTime GetDateFromString(string dateStr)
        {
            var validDate = DateTime.MinValue;
            try { validDate = DateTime.Parse(dateStr); }
            catch (Exception ex)
            {
                DateTime.TryParseExact(dateStr, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out validDate);
            }
            return validDate;
        }


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
        private string GetUserId()
        {
            if (User == null)
                return string.Empty;

            if (!string.IsNullOrEmpty(User.Identity.GetUserId()))
                return User.Identity.GetUserId();

            var email = ClaimsPrincipal.Current.Claims.Where(c => c.Type == "Email").Select(s => s.Value).FirstOrDefault();

            var user = AppUserManager.FindByNameOrEmailOrPhone(email, OwnerInfo.ApplicationOwnerKey, OwnerInfo.DataOwnerKey);

            return user.Id;
        }
    }
}