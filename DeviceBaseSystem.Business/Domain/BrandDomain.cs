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
    public class BrandDomain : BusinessDomainV3<Brand>, IBusinessDomainV3<Brand>
    {
        #region Ctors
        public BrandDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public BrandDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(Brand currentBrand, Brand item)
        {
            if (currentBrand != null)
            {
                currentBrand.LastUpdate = DateTime.Now;
                currentBrand.BrandName = item.BrandName;
                currentBrand.BrandCode = item.BrandCode;                    
                MainRepository.Update(currentBrand);
            }
            else
            {
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }
        public async Task DeleteBrands(List<Brand> datas)
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
