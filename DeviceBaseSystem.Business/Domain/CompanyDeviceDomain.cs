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
    public class CompanyDeviceDomain : BusinessDomainV3<CompanyDevice>, IBusinessDomainV3<CompanyDevice>
    {
        #region Ctors
        public CompanyDeviceDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public CompanyDeviceDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(CompanyDevice current, CompanyDevice item)
        {
            if (current != null)
            {
                current.LastUpdate = DateTime.Now;
                current.IMEI = item.IMEI;
                current.Description = item.Description;                    
                current.DeviceModelId = item.DeviceModelId;
                current.CompanyId = item.CompanyId;                    
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

        public async Task Delete(List<CompanyDevice> datas)
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
