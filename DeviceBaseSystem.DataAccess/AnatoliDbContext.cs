using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.DataAccess.Models;

namespace DeviceBaseSystem.DataAccess
{
    public class AnatoliDbContext : IdentityDbContext<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        #region Properties
        //public DbSet<DeviceBaseSystemRulDef> DeviceBaseSystemRulDefs { get; set; }

        #region Identity
        public DbSet<ApplicationOwner> ApplicationOwners { get; set; }
        public DbSet<DataOwner> DataOwners { get; set; }
        public DbSet<DataOwnerCenter> DataOwnerCenters { get; set; }
        public DbSet<AnatoliAccount> AnatoliAccounts { get; set; }
        public DbSet<AnatoliContact> AnatoliContacts { get; set; }
        public DbSet<AnatoliContactType> AnatoliContactTypes { get; set; }
        public DbSet<AnatoliPlace> AnatoliPlaces { get; set; }
        public DbSet<AnatoliRegion> AnatoliRegions { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<IdentityUserRole> UserRoles { get; set; }
        public DbSet<Principal> Principals { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionCatalog> PermissionCatalogs { get; set; }
        public DbSet<PermissionAction> PermissionActions { get; set; }
        public DbSet<ApplicationModuleResource> ApplicationModuleResources { get; set; }
        public DbSet<ApplicationModule> ApplicationModules { get; set; }
        public DbSet<PrincipalPermission> PrincipalPermissions { get; set; }
        #endregion

        #endregion

        #region ctors

        public AnatoliDbContext()
            : base("Name=DeviceBaseSystemConnectionString")
        {
        }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        public static AnatoliDbContext Create()
        {
            return new AnatoliDbContext();
        }
    }
}
