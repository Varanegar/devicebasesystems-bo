using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;
using Anatoli.Common.DataAccess.Models;
using DeviceBaseSystem.DataAccess;
using DeviceBaseSystem.DataAccess.Models;

namespace DeviceBaseSystem.Business.Domain
{
    public class CompanyDomain : BusinessDomainV3<Company>, IBusinessDomainV3<Company>
    {
        #region Ctors
        public CompanyDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public CompanyDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(Company current, Company item)
        {
            if (current != null)
            {
                current.LastUpdate = DateTime.Now;
                current.CompanyName = item.CompanyName;
                current.CompanyCode = item.CompanyCode;                    
                MainRepository.Update(current);
            }
            else
            {
                if (item.Id == Guid.Empty)
                    item.Id = Guid.NewGuid();
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }
        public async Task Delete(List<Company> datas)
        {
            //Validate

            await DeleteAsync(datas);
        }

        public override void SetConditionForFetchingData()
        {
            MainRepository.ExtraPredicate = p => true;
        }
        #endregion
    }
}
