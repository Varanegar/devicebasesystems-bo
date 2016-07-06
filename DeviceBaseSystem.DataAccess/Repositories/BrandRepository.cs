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
    public class BrandRepository : AnatoliRepository<Brand>, IBrandRepository
    {
        #region Ctors
        public BrandRepository() : this(new AnatoliDbContext()) { }
        public BrandRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        public BrandRepository(AnatoliDbContext context, OwnerInfo OwnerInfo)
            : base(context, OwnerInfo)
        {
        }
        #endregion
    }
}
