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
    public class DeviceModelRepository : AnatoliRepository<DeviceModel>, IDeviceModelRepository
    {
        #region Ctors
        public DeviceModelRepository() : this(new AnatoliDbContext()) { }
        public DeviceModelRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        public DeviceModelRepository(AnatoliDbContext context, OwnerInfo OwnerInfo)
            : base(context, OwnerInfo)
        {
        }
        #endregion
    }
}
