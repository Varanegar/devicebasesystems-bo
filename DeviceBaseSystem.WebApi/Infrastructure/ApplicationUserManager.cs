using System;
using Microsoft.Owin;
using Anatoli.DataAccess;
using System.Threading.Tasks;
using Anatoli.Business.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Anatoli.DataAccess.Models.Identity;
using DeviceBaseSystem.DataAccess;
using DeviceBaseSystem.DataAccess.Repositories;

namespace DeviceBaseSystem.WebApi.Infrastructure
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public User FindByNameOrEmailOrPhone(string usernameOrEmailOrPhone, Guid applicationOwner, Guid dataOwnerKey)
        {
            return new UserDomain(applicationOwner, dataOwnerKey).FindByNameOrEmailOrPhone(usernameOrEmailOrPhone);
        }
        public async Task<User> FindByNameOrEmailOrPhoneAsync(string usernameOrEmailOrPhone, Guid applicationOwner, Guid dataOwnerKey)
        {
            return await new UserDomain(applicationOwner, dataOwnerKey).FindByNameOrEmailOrPhoneAsync(usernameOrEmailOrPhone);
        }

        public async Task<User> FindByNameOrEmailOrPhoneAsync(string usernameOrEmailOrPhone, string password, Guid applicationOwner, Guid dataOwnerKey)
        {
            var userDomain = new UserDomain(applicationOwner, dataOwnerKey);

            var user = await userDomain.FindByNameOrEmailOrPhoneAsync(usernameOrEmailOrPhone);

            if (user != null)
                return await FindAsync(user.UserName, password);
            else
                return null;
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {

            var appDbContext = context.Get<AnatoliDbContext>();
            var appUserManager = new ApplicationUserManager(new AnatoliUserStore(appDbContext));

            // Configure validation logic for usernames
            appUserManager.UserValidator = new UserValidator<User>(appUserManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false

            };

            // Configure validation logic for passwords
            appUserManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            appUserManager.EmailService = new Services.EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                appUserManager.UserTokenProvider = new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }

            return appUserManager;
        }
    }
}