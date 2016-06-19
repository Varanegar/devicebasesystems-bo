using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Anatoli.DataAccess.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DeviceBaseSystem.DataAccess.Repositories
{
    public class AnatoliUserStore : UserStore<User, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUserStore<User>, IDisposable

        //IUserStore<User>, IUserPasswordStore<User>, IDisposable
    {
        #region Properties
        UserRepository UserRepository { get; set; }
        AnatoliDbContext context { get; set; }
        #endregion

        #region Ctors
        public AnatoliUserStore(AnatoliDbContext context)
            : base(context)
        {
            this.context = context;
            UserRepository = new UserRepository(context);
        }

        //public AnatoliUserStore(AnatoliDbContext dbc)
        //    : this(new UserRepository(dbc))
        //{ }
        //public AnatoliUserStore(UserRepository userRepository)
        //{
        //    UserRepository = userRepository;
        //}
        #endregion

        #region User Store
        public async Task CreateAsync(User user)
        {
            user.CreatedDate = user.LastUpdate = user.LastEntry = DateTime.Now;

            await UserRepository.AddAsync(user);

            await UserRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            await UserRepository.DeleteAsync(user);

            await UserRepository.SaveChangesAsync();
        }

        public async Task ChangeEmailAddress(User user, string email)
        {
            user.Email = email;
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            await UserRepository.UpdateAsync(user);
            await UserRepository.SaveChangesAsync();
        }

        public async Task<bool> ResetPasswordByCodeAsync(User user, string password, string code)
        {
            if (code == user.ResetSMSCode && user.ResetSMSRequestTime != null && user.ResetSMSRequestTime > DateTime.Now.AddMinutes(-5))
            {
                user.PasswordHash = password;
                user.EmailConfirmed = true;
                user.PhoneNumberConfirmed = true;
                await UserRepository.UpdateAsync(user);
                await UserRepository.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool> VerifySMSCodeAsync(User user, string code)
        {
            if (code == user.ResetSMSCode && user.ResetSMSRequestTime != null && user.ResetSMSRequestTime > DateTime.Now.AddMinutes(-5))
            {
                if (user.ResetSMSPass != null)
                    user.PasswordHash = user.ResetSMSPass;
                user.EmailConfirmed = true;
                user.PhoneNumberConfirmed = true;
                await UserRepository.UpdateAsync(user);
                await UserRepository.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task SetResetSMSCodeAsync(User user, string smsCode, string newPassword)
        {
            user.ResetSMSCode = smsCode;
            user.ResetSMSPass = newPassword;
            user.ResetSMSRequestTime = DateTime.Now;
            await UserRepository.UpdateAsync(user);
            await UserRepository.SaveChangesAsync();
        }

        public override async Task<User> FindByIdAsync(string userId)
        {
            var model = await UserRepository.FindAsync(p => p.Id == userId);

            return model;
        }

        public override async Task<User> FindByNameAsync(string userName)
        {
            var model = await UserRepository.FindAsync(p => p.UserName == userName);

            return model;
        }

        public async Task<User> FindByPhoneAsync(string phone)
        {
            var model = await UserRepository.FindAsync(p => p.PhoneNumber == phone);

            return model;
        }

        public override async Task<User> FindByEmailAsync(string email)
        {
            var model = await UserRepository.FindAsync(p => p.Email == email);

            return model;
        }

        public override async Task UpdateAsync(User user)
        {
            await UserRepository.UpdateAsync(user);

            await UserRepository.SaveChangesAsync();
        }
        #endregion

        #region User Password Store
        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(string.IsNullOrEmpty(user.Password));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.Password = passwordHash;

            return Task.FromResult(0);
        }
        #endregion

        public void Dispose()
        {
            UserRepository.Dispose();

            UserRepository = null;
        }
    }
}
