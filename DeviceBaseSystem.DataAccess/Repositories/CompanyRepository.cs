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
    public class CompanyRepository : AnatoliRepository<Company>, ICompanyRepository
    {
        #region Ctors
        public CompanyRepository() : this(new AnatoliDbContext()) { }
        public CompanyRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        public CompanyRepository(AnatoliDbContext context, OwnerInfo OwnerInfo)
            : base(context, OwnerInfo)
        {
        }
        #endregion
    }
}
