using System;
using System.Web.Http;
using IdentityModel.Client;
using System.Threading.Tasks;
using Anatoli.Business.Domain;
using Anatoli.DataAccess.Models.Identity;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Anatoli.ViewModels.User;
using Anatoli.DataAccess;
using Anatoli.Business.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data;
using Newtonsoft.Json;
using Anatoli.Common.WebApi;
using DeviceBaseSystem.WebApi.Classes;
using Anatoli.ViewModels;
using DeviceBaseSystem.DataAccess.Repositories;
using DeviceBaseSystem.DataAccess;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/identityAccounts")]
    public class IdentityAccountsController : AnatoliApiController
    {
        #region Properties
        private AnatoliDbContext CurrentContext
        {
            get
            {
                return Request.GetOwinContext().Get<AnatoliDbContext>();
            }
        }
        public bool UseIdentityServer
        {
            get
            {
                var useIdentityServer = false;

                bool.TryParse(ConfigurationManager.AppSettings["UseIdentityServer"], out useIdentityServer);

                return useIdentityServer;
            }
        }
        #endregion

        #region InnerClasses
        public class RequestModel
        {
            public string Username
            {
                get;
                set;
            }
            public string Password
            {
                get;
                set;
            }

            public string Scope { get; set; }
        }
        #endregion

        #region Actions

        #region Login
        [AllowAnonymous, HttpPost, Route("login")]
        public async Task<IHttpActionResult> Login([FromBody] RequestModel model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                    return StatusCode(System.Net.HttpStatusCode.NotFound);

                if (string.IsNullOrEmpty(model.Scope))
                    return BadRequest("نرم افزار کاربر مشخص نشده است");

                var additionalData = model.Scope.Split(',');
                var appOwner = Guid.Empty;
                var dataOwner = Guid.Empty;

                if (!Guid.TryParse(additionalData[0].Trim(), out appOwner))
                    return BadRequest("نرم افزار کاربر مشخص نشده است");

                if (!Guid.TryParse(additionalData[1].Trim(), out dataOwner))
                    return BadRequest("شرکت کاربر مشخص نشده است");


                //To be sure the user data is exist & correct.
                var user = await CheckUserDataInCloud(model, appOwner, dataOwner);
                if (user == null)
                    return BadRequest("اطلاعات کاربری یافت نشد، لطفا ابتدا ثبت نام نمایید.");

                var _userRoleIds = user.Roles.Select(s => s.RoleId).ToList();

                var roles = AppRoleManager.Roles
                                          .Where(p => _userRoleIds.Contains(p.Id))
                                          .Select(s => s.Name)
                                          .ToArray();

                if (UseIdentityServer)
                    await userNotExistInIdentityThenRegisterIt(user, model, string.Join(",", roles));


                var token = await GetToken(model.Username, model.Password);

                if (!string.IsNullOrEmpty(token.Error))
                    return Unauthorized();

                return Ok(new
                {
                    access_token = token.AccessToken,
                    userName = model.Username,
                    token = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest("لطفا دوباره تلاش نمایید");
            }
        }

        private async Task userNotExistInIdentityThenRegisterIt(User user, RequestModel data, string roles)
        {
            //check user data exist in identity server, if not register it.
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["IdentityServerUrl"]);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", data.Username),
                    new KeyValuePair<string, string>("password", data.Password),
                    new KeyValuePair<string, string>("fullname", user.FullName),
                    new KeyValuePair<string, string>("email", user.Email),
                    new KeyValuePair<string, string>("emailConfirmed", user.EmailConfirmed.ToString()),
                    new KeyValuePair<string, string>("phonenumber", user.PhoneNumber),
                    new KeyValuePair<string, string>("phonenumberConfirmed", user.PhoneNumberConfirmed.ToString()),
                    new KeyValuePair<string, string>("roles", roles),
                });

                await client.PostAsync("/api/account/signupUserofCloud", formContent);
            }
        }

        private async Task<TokenResponse> GetToken(string user, string password)
        {
            var client = new TokenClient("https://localhost:44300/core/connect/token", "anatoliCloudClient", "anatoliCloudClient");

            var result = await client.RequestResourceOwnerPasswordAsync(user, password, "read write webapis offline_access openid email roles profile");

            return result;
        }

        private async Task<User> CheckUserDataInCloud(RequestModel data, Guid ownerKey, Guid dataOwnerKey)
        {
            var user = await new UserDomain(ownerKey, dataOwnerKey).FindByNameOrEmailOrPhoneAsync(data.Username);

            if (user == null)
                return null;

            return await AppUserManager.FindAsync(user.UserName, data.Password);
        }
        #endregion

        #region RegisterUser
        [Authorize(Roles = "AuthorizedApp")]
        [Route("saveUser"), HttpPost]
        public async Task<IHttpActionResult> SaveUser([FromBody] BaseRequestModel model)
        {
            var userModel = JsonConvert.DeserializeObject<CreateUserBindingModel>(model.user);

            if (userModel.UniqueId != Guid.Empty && userModel.UniqueId != null)
                return await UpdateUser(userModel);
            else
                return await Register(userModel, false);
        }

        private async Task<IHttpActionResult> UpdateUser(CreateUserBindingModel model)
        {
            var user = await new UserDomain(OwnerKey, DataOwnerKey, CurrentContext).GetByIdAsync(model.UniqueId.ToString());

            if (!string.IsNullOrEmpty(model.FullName))
                user.FullName = model.FullName;

            user.SecurityStamp = Guid.NewGuid().ToString();

            if (!string.IsNullOrEmpty(model.Password) && model.Password == model.ConfirmPassword)
                user.PasswordHash = AppUserManager.PasswordHasher.HashPassword(model.Password);

            user.PhoneNumberConfirmed = user.EmailConfirmed = true;

            await new AnatoliUserStore(CurrentContext).UpdateAsync(user);

            if (UseIdentityServer)
                await UpdateIdentityServerUser(model, user.UserNameStr);

            return Ok(model);
        }

        private async Task UpdateIdentityServerUser(CreateUserBindingModel model, string username)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["IdentityServerUrl"]);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("fullName", model.FullName),
                    new KeyValuePair<string, string>("password", model.Password),
                });

                await client.PostAsync("/api/account/updateUser", formContent);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("register"), HttpPost]
        public async Task<IHttpActionResult> Register(CreateUserBindingModel model, bool isCustomer = true)
        {
            var user = await new UserDomain(OwnerKey, DataOwnerKey).FindByNameOrEmailOrPhoneAsync(model.Username);

            if (user != null)
                return BadRequest("اطلاعات کاربری قبلا ثبت گردیده است، لطفا لاگین نمایید.");

            return await RegisterUserOnCloudnIdentityServer(model, isCustomer);
        }

        private async Task<IHttpActionResult> RegisterUserOnCloudnIdentityServer(CreateUserBindingModel model, bool isCustomer = true)
        {
            using (var transaction = CurrentContext.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    User user;
                    Principal userPrincipal;

                    ConvertModels(model, out user, out userPrincipal);

                    await new UserDomain(OwnerKey, DataOwnerKey, CurrentContext).SavePerincipal(userPrincipal);

                    var addUserResult = await AppUserManager.CreateAsync(user, model.Password);

                    if (!addUserResult.Succeeded)
                        return GetErrorResult(addUserResult);

                    var roles = new List<string> { "User" };

                    if (!string.IsNullOrEmpty(model.RoleName))
                        roles.Add(model.RoleName);

                    await TryAddRoles(roles);

                    await AppUserManager.AddToRolesAsync(user.Id, roles.ToArray());

                    if (UseIdentityServer)
                    {
                        var result = await RegisterUserInIdentityServer(user, model.RoleName);

                        if (!result)
                        {
                            transaction.Rollback();

                            return BadRequest("عملیات ثبت با شکست مواجه گردید");
                        }
                    }


                    var locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

                    return Created(locationHeader, TheModelFactory.Create(user));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return GetErrorResult(ex);
                }
            }
        }


        private void ConvertModels(CreateUserBindingModel model, out User user, out Principal userPrincipal)
        {
            var id = Guid.NewGuid();

            user = new User
            {
                Id = id.ToString(),
                UserName = (OwnerKey == DataOwnerKey) ? model.Username : id.ToString(),
                Email = model.Email,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                CreatedDate = DateTime.Now,
                PhoneNumber = model.Mobile,
                ApplicationOwnerId = OwnerKey,
                DataOwnerId = DataOwnerKey,
                UserNameStr = model.Username
            };

            userPrincipal = new Principal
            {
                Id = Guid.NewGuid(),
                Title = user.UserNameStr,
                ApplicationOwnerId = (Guid)user.ApplicationOwnerId
            };

            user.PrincipalId = userPrincipal.Id;
        }

        /// <summary>
        /// Try adding roles if not exist.
        /// </summary>
        /// <param name="roleNames"></param>
        /// <returns></returns>
        private async Task TryAddRoles(List<string> roleNames)
        {
            var roles = AppRoleManager.Roles.ToList();
            foreach (var role in roleNames)
                if (!roles.Any(p => p.Name == role))
                    await AppRoleManager.CreateAsync(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = role });
        }

        private async Task SendSmsNotification(CreateUserBindingModel model, AnatoliDbContext context, User user)
        {
            var hashedNewPassword = AppUserManager.PasswordHasher.HashPassword(model.Password);

            await new SMSManager().SendResetPasswordSMS(user, context, hashedNewPassword,
                                                        model.SendPassSMS ? SMSManager.SMSBody.NEW_USER : SMSManager.SMSBody.NEW_USER_BACKOFFICE);
        }

        private async Task<bool> RegisterUserInIdentityServer(User user, string role)
        {
            var flgUserInIdentityServer = await UserExistInIdentityServer(user.UserNameStr, user.Email, user.PhoneNumber);

            if (flgUserInIdentityServer)
                return flgUserInIdentityServer;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["IdentityServerUrl"]);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", user.UserName),
                    new KeyValuePair<string, string>("password", user.Password),
                    new KeyValuePair<string, string>("fullname", user.FullName),
                    new KeyValuePair<string, string>("email", user.Email),
                    new KeyValuePair<string, string>("emailConfirmed", user.EmailConfirmed.ToString()),
                    new KeyValuePair<string, string>("phonenumber", user.PhoneNumber),
                    new KeyValuePair<string, string>("phonenumberConfirmed", user.PhoneNumberConfirmed.ToString()),
                    new KeyValuePair<string, string>("roles", "User,"+ role),
                });

                var response = await client.PostAsync("/api/account/signupUserofCloud", formContent);

                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
        }

        private async Task<bool> UserExistInIdentityServer(string username, string email, string phone)
        {
            //check user data exist in identity server
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["IdentityServerUrl"]);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("email", email),
                    new KeyValuePair<string, string>("phonenumber", phone),
                });

                var response = await client.PostAsync("/api/account/checkUserExist", formContent);

                var res = await response.Content.ReadAsAsync<dynamic>();

                return res.Result;
            }
        }
        #endregion

        [Route("ConfirmEmail"), HttpGet, AllowAnonymous]
        public async Task<IHttpActionResult> ConfirmEmail([FromBody] UserRequestModel data)
        {
            if (string.IsNullOrWhiteSpace(data.userId) || string.IsNullOrWhiteSpace(data.code))
            {
                ModelState.AddModelError("", "User Id and Code are required");

                return BadRequest(ModelState);
            }

            var result = await AppUserManager.ConfirmEmailAsync(data.userId, data.code);

            if (result.Succeeded)
            {
                var user = await AppUserManager.FindByIdAsync(data.userId);

                if (UseIdentityServer)
                    await ConfirmIdentityUserEmail(user.UserNameStr);

                return Ok();
            }
            else
                return GetErrorResult(result);
        }

        private async Task ConfirmIdentityUserEmail(string username)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["IdentityServerUrl"]);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username),
                });

                await client.PostAsync("/api/account/confirmEmail", formContent);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ConfirmMobile")]
        public async Task<IHttpActionResult> ConfirmPhoneNumber([FromBody] UserRequestModel data)
        {
            var userStore = new AnatoliUserStore(CurrentContext);

            var userInfo = await GetUserByUserName(data.username);

            var user = await userStore.FindByIdAsync(userInfo.Id);

            var result = await userStore.VerifySMSCodeAsync(user, data.code);

            if (result)
            {
                if (UseIdentityServer)
                    await ConfirmPhoneNumberIdentityUser(data.username);

                return Ok(new BaseViewModel());
            }
            else
                return BadRequest("کد نامعتبر است یا زمان مجاز آن به پایان رسیده است");
        }

        private async Task ConfirmPhoneNumberIdentityUser(string username)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["IdentityServerUrl"]);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username),
                });

                await client.PostAsync("/api/account/confirmPhoneNumber", formContent);
            }
        }

        private async Task<User> GetUserByUserName(string username)
        {
            return await new UserDomain(OwnerKey, DataOwnerKey, CurrentContext).GetByUsernameAsync(username);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResendPassCode")]
        public async Task<IHttpActionResult> ResendPassCode([FromBody] UserRequestModel data)
        {
            try
            {
                var user = await GetUserByUserName(data.username);

                await new SMSManager().SendResetPasswordSMS(user, CurrentContext, null, SMSManager.SMSBody.NEW_USER_FORGET_PASSWORD);

                return Ok(new BaseViewModel());
            }
            catch (Exception ex)
            {
                log.Error(ex, "Web API Call Error in ResendPassCode. ", data.username);

                return GetErrorResult(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SendPassCode")]
        public async Task<IHttpActionResult> SendPassCode([FromBody] UserRequestModel data)
        {
            try
            {
                var user = await GetUserByUserName(data.username);

                if (user == null)
                    return GetErrorResult("کاربر یافت نشد");

                await new SMSManager().SendResetPasswordSMS(user, CurrentContext, null, SMSManager.SMSBody.NEW_USER_FORGET_PASSWORD);

                return Ok(new BaseViewModel());
            }
            catch (Exception ex)
            {
                log.Error(ex, "Web API Call Error in SendPassCode. ", data.username);

                return GetErrorResult(ex); ;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword([FromBody] UserRequestModel data)
        {
            var user = await GetUserByUserName(data.username);

            if (user == null)
                return GetErrorResult("کاربر یافت نشد");

            var hashedNewPassword = AppUserManager.PasswordHasher.HashPassword(data.password);

            await new SMSManager().SendResetPasswordSMS(user, CurrentContext, hashedNewPassword, SMSManager.SMSBody.NEW_USER_FORGET_PASSWORD);

            return Ok(new BaseViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPasswordByCode")]
        public async Task<IHttpActionResult> ResetPasswordByCode([FromBody] UserRequestModel data)
        {
            var userStore = new AnatoliUserStore(CurrentContext);

            var user = await GetUserByUserName(data.username);

            if (user == null)
                return GetErrorResult("کاربر یافت نشد");

            user.SecurityStamp = Guid.NewGuid().ToString();

            var hashedNewPassword = AppUserManager.PasswordHasher.HashPassword(data.password);

            bool result = await userStore.ResetPasswordByCodeAsync(user, hashedNewPassword, data.code);

            if (result)
            {
                if (UseIdentityServer)
                    await ResetPasswordByCodeInIdentityServer(data.username, data.password);

                return Ok(new BaseViewModel());
            }
            else
                return BadRequest("کد نامعتبر است یا زمان مجاز آن به پایان رسیده است");
        }

        private async Task ResetPasswordByCodeInIdentityServer(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["IdentityServerUrl"]);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password),
                });

                await client.PostAsync("/api/account/resetPasswordByCode", formContent);
            }
        }

        [Authorize(Roles = "AuthorizedApp,User")]
        [Route("ChangePassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await AppUserManager.ChangePasswordAsync(CurrentUserId, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
                return GetErrorResult(result);

            if (UseIdentityServer)
                await ChangePasswordInIdentiServer(User.GetISUserId(), model.OldPassword, model.NewPassword);

            model.NewPassword = model.OldPassword = model.ConfirmPassword = "****";

            return Ok(model);
        }

        private async Task ChangePasswordInIdentiServer(string userId, string oldPassword, string newPassword)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["IdentityServerUrl"]);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("userId", userId),
                    new KeyValuePair<string, string>("oldPassword", oldPassword),
                    new KeyValuePair<string, string>("password", newPassword),
                });

                await client.PostAsync("/api/account/changePassword", formContent);
            }
        }
        #endregion
    }
}