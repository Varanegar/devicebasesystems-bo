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
    public class DeviceModelDomain : BusinessDomainV3<DeviceModel>, IBusinessDomainV3<DeviceModel>
    {
        #region Ctors
        public DeviceModelDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public DeviceModelDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(DeviceModel current, DeviceModel item)
        {
            if (current != null)
            {
                current.LastUpdate = DateTime.Now;
                current.DeviceCode = item.DeviceCode;
                current.DeviceName = item.DeviceName;
                current.BrandId = item.BrandId;
                MainRepository.Update(current);
            }
            else
            {
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                if (item.Id == Guid.Empty)
                    item.Id = Guid.NewGuid();
                MainRepository.Add(item);
            }
        }
        public async Task Delete(List<DeviceModel> datas)
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
