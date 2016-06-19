using Anatoli.Common.DataAccess.Repositories;
using DeviceBaseSystem.DataAccess.Interfaces;
using Anatoli.DataAccess.Models.Identity;

namespace DeviceBaseSystem.DataAccess.Repositories
{
    public class PrincipalRepository : BaseAnatoliRepository<Principal>, IPrincipalRepository
    {
        public PrincipalRepository() : this(new AnatoliDbContext()) { }
        public PrincipalRepository(AnatoliDbContext context)
            : base(context)
        {
        }
    }
}
