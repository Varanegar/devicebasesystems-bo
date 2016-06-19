using Microsoft.Owin;
using Anatoli.DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using DeviceBaseSystem.DataAccess;

namespace DeviceBaseSystem.WebApi.Infrastructure
{
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var appRoleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<AnatoliDbContext>()));

            return appRoleManager;
        }
    }
}