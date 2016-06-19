using NLog;
using System;
using System.Threading.Tasks;
using DeviceBaseSystem.DataAccess.Repositories;
using DeviceBaseSystem.DataAccess;
using System.Linq;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.Business.Domain
{
    public class UserDomain
    {
        #region Properties
        protected static Logger Logger { get; set; }
        public Guid ApplicationOwnerKey { get; protected set; }
        public Guid DataOwnerKey { get; protected set; }
        public virtual UserRepository UserRepository { get; set; }
        public virtual PrincipalRepository PrincipalRepository { get; set; }
        public AnatoliDbContext DBContext { get; set; }
        #endregion

        #region Ctors
        public UserDomain(Guid applicationOwnerKey, Guid dataOwnerKey)
            : this(applicationOwnerKey, dataOwnerKey, new AnatoliDbContext())
        {

        }
        public UserDomain(Guid applicationOwnerKey, Guid dataOwnerKey, AnatoliDbContext dbc)
        {
            UserRepository = new UserRepository(dbc);
            PrincipalRepository = new PrincipalRepository(dbc);
            ApplicationOwnerKey = applicationOwnerKey;
            DataOwnerKey = dataOwnerKey;
            Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());
        }
        #endregion

        #region Methods
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await UserRepository.FindAsync(p => p.UserNameStr == username && p.ApplicationOwnerId == ApplicationOwnerKey && DataOwnerKey == p.DataOwnerId);
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            return await UserRepository.FindAsync(p => p.Id == userId && p.ApplicationOwnerId == ApplicationOwnerKey && DataOwnerKey == p.DataOwnerId);
        }

        public async Task<User> GetByPhoneAsync(string phone)
        {
            return await UserRepository.FindAsync(p => p.PhoneNumber == phone && p.ApplicationOwnerId == ApplicationOwnerKey && DataOwnerKey == p.DataOwnerId);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await UserRepository.FindAsync(p => p.Email == email && p.ApplicationOwnerId == ApplicationOwnerKey && DataOwnerKey == p.DataOwnerId);
        }

        public async Task<User> UserExists(string email, string phone, string username)
        {
            return await UserRepository.FindAsync(p => (p.Email == email || p.PhoneNumber == phone || p.UserNameStr == username) && p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey);
        }

        public User FindByNameOrEmailOrPhone(string usernameOrEmailOrPhone)
        {
            return UserRepository.GetQuery()
                                 .Where(p => (p.Email == usernameOrEmailOrPhone ||
                                              p.PhoneNumber == usernameOrEmailOrPhone ||
                                              p.UserNameStr == usernameOrEmailOrPhone) &&
                                              p.ApplicationOwnerId == ApplicationOwnerKey &&
                                              p.DataOwnerId == DataOwnerKey)
                                .FirstOrDefault();
        }
        public async Task<User> FindByNameOrEmailOrPhoneAsync(string usernameOrEmailOrPhone)
        {
            return await UserRepository.FindAsync(p => (p.Email == usernameOrEmailOrPhone || p.PhoneNumber == usernameOrEmailOrPhone || p.UserNameStr == usernameOrEmailOrPhone) && p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey);
        }

        public async Task SavePerincipal(Principal principal)
        {
            try
            {
                await PrincipalRepository.AddAsync(principal);

                await PrincipalRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        #endregion
    }
}
