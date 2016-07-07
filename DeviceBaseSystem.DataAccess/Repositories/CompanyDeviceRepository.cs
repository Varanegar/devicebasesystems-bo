using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.Common.DataAccess.Models;
using Anatoli.Common.DataAccess.Repositories;
using DeviceBaseSystem.DataAccess.Interfaces;
using DeviceBaseSystem.DataAccess.Models;

namespace DeviceBaseSystem.DataAccess.Repositories
{
    public class CompanyDeviceRepository : AnatoliRepository<CompanyDevice>, ICompanyDeviceRepository
    {
        #region Ctors
        public CompanyDeviceRepository() : this(new AnatoliDbContext()) { }
        public CompanyDeviceRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        public CompanyDeviceRepository(AnatoliDbContext context, OwnerInfo OwnerInfo)
            : base(context, OwnerInfo)
        {
        }
        #endregion
    }
}
