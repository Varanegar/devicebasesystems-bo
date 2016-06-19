using NLog;
using Owin;
using System;
using System.Linq;
using Microsoft.Owin;
using System.Web.Http;
using Anatoli.DataAccess;
using System.Data.Entity;
using System.Configuration;
using Microsoft.Owin.Security;
using WebApiContrib.Formatting;
using System.Net.Http.Formatting;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using DeviceBaseSystem.WebApi.Handler;
using Newtonsoft.Json.Serialization;
using Microsoft.Owin.Security.OAuth;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin.Security.DataHandler.Encoder;
using DeviceBaseSystem.DataAccess;
using DeviceBaseSystem.WebApi.Infrastructure;

[assembly: OwinStartup(typeof(DeviceBaseSystem.WebApi.Startup))]
namespace DeviceBaseSystem.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AnatoliDbContext, DataAccess.Migrations.Configuration>());

            var tempData = new AnatoliDbContext().Applications.FirstOrDefault();

            LogManager.ReconfigExistingLoggers();

            ConfigureOAuthTokenGeneration(app);
            ConfigureOAuthTokenConsumption(app);

            ///*****************************************************************/
            if (bool.Parse(ConfigurationManager.AppSettings["UseIdentityServer"]))
            {
                JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

                app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
                {
                    Authority = "https://localhost:44300/core",
                    RequiredScopes = new[] { "write" },

                    // client credentials for the introspection endpoint
                    ClientId = "write",
                    ClientSecret = "secret"
                });
            }
            ///*****************************************************************/

            var httpConfig = new HttpConfiguration();
            ConfigureWebApi(httpConfig);

            ConfigureAutoMapper();
            //ConfigureUserinfo();

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(httpConfig);

            httpConfig.EnsureInitialized();
        }

        private void ConfigureUserinfo()
        {
            /*
            AnatoliDbContext context = new AnatoliDbContext();

            var manager = new ApplicationUserManager(new AnatoliUserStore(context));
            if (manager.Users.Count() > 0) return;
            var roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context));


            var id = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16");
            var user = new User()
            {
                Id = id.ToString(),
                UserName = "anatoli",
                Email = "anatoli@varanegar.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                CreatedDate = DateTime.Now,
                PhoneNumber = "87135000",
                ApplicationOwnerId = new Principal { Id = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16") },
                Principal_Id = Guid.Parse("02D3C1AA-6149-4810-9F83-DF3928BFDF16")
            };


            var result = manager.CreateAsync(user, "anatoli@vn@87134").Result;

            if (roleManager.Roles.Count() == 0)
            {
                result = roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" }).Result;
                result = roleManager.CreateAsync(new IdentityRole { Name = "Admin" }).Result;
                result = roleManager.CreateAsync(new IdentityRole { Name = "AuthorizedApp" }).Result;
                result = roleManager.CreateAsync(new IdentityRole { Name = "User" }).Result;
            }

            var adminUser = manager.FindByNameAsync("anatoli").Result;

            result = manager.AddToRolesAsync(adminUser.Id, new string[] { "SuperAdmin", "Admin", "AuthorizedApp", "User" }).Result;

            id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");
            var user2 = new User()
            {
                Id = id.ToString(),
                UserName = "petropay",
                Email = "petropay@varanegar.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = "02100000000",
                CreatedDate = DateTime.Now,
                ApplicationOwnerId = new Principal { Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C") },
                Principal_Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C")
            };

            result = manager.CreateAsync(user2, "petropay@webapp").Result;

            var userInfo2 = manager.FindByNameAsync("petropay").Result;

            result = manager.AddToRolesAsync(userInfo2.Id, new string[] { "AuthorizedApp", "User" }).Result;

            id = Guid.Parse("0DAB1636-AE22-4ABE-A18D-6EC7B8E9C544");
            var user3 = new User()
            {
                Id = id.ToString(),
                UserName = "AnatoliMobileApp",
                Email = "anatoli-mobile-app@varanegar.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = "09125793221",
                CreatedDate = DateTime.Now,
                ApplicationOwnerId = new Principal { Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C") },
                Principal_Id = Guid.Parse("0DAB1636-AE22-4ABE-A18D-6EC7B8E9C544")
            };

            result = manager.CreateAsync(user3, "Anatoli@App@Vn").Result;
            userInfo2 = manager.FindByNameAsync("AnatoliMobileApp").Result;
            result = manager.AddToRolesAsync(userInfo2.Id, new string[] { "AuthorizedApp", "User" }).Result;

            id = Guid.Parse("33FA710A-B1E6-4765-8719-0DD1589E8F8B");
            var user4 = new User()
            {
                Id = id.ToString(),
                UserName = "AnatoliSCM",
                Email = "anatoli-scm@varanegar.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = "09125793221",
                CreatedDate = DateTime.Now,
                ApplicationOwnerId = new Principal { Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C") },
                Principal_Id = Guid.Parse("33FA710A-B1E6-4765-8719-0DD1589E8F8B")
            };

            result = manager.CreateAsync(user4, "Anatoli@App@Vn").Result;
            userInfo2 = manager.FindByNameAsync("AnatoliMobileApp").Result;
            result = manager.AddToRolesAsync(userInfo2.Id, new string[] { "AuthorizedApp", "User" }).Result;

            id = Guid.Parse("95FCB850-2E63-4B26-8DBF-BBC86B7F5046");
            var user5 = new User()
            {
                Id = id.ToString(),
                UserName = "AnatoliInterCom",
                Email = "anatoli-inter-com@varanegar.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = "09125793221",
                CreatedDate = DateTime.Now,
                ApplicationOwnerId = new Principal { Id = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C") },
                Principal_Id = Guid.Parse("95FCB850-2E63-4B26-8DBF-BBC86B7F5046")
            };

            result = manager.CreateAsync(user5, "Anatoli@App@Vn").Result;
            userInfo2 = manager.FindByNameAsync("AnatoliInterCom").Result;
            result = manager.AddToRolesAsync(userInfo2.Id, new string[] { "AuthorizedApp", "User" }).Result;
             * */
        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(AnatoliDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(365),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat(ConfigurationManager.AppSettings["server:URI"])
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {

            var issuer = ConfigurationManager.AppSettings["server:URI"];
            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                    }
                });
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            //config.Filters.Add(new LoggingFilterAttribute());
            //config.MessageHandlers.Add(new WrappingHandler());
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.Add(new ProtoBufFormatter());
        }

        private void ConfigureAutoMapper()
        {
            ConfigDefaultAutoMapperHelper.Config();
        }
    }
}