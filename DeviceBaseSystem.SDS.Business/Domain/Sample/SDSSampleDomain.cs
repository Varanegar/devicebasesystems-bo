using Anatoli.SDS.Business;
using Anatoli.SDS.Business.Proxy.Interfaces;
using Anatoli.SDS.ViewModels;
using Anatoli.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.PMC.Business.Domain.Store
{
    public class SDSSampleDomain : SDSBusinessDomain<SDSBaseViewModel, BaseViewModel>, ISDSBusinessDomain<SDSBaseViewModel, BaseViewModel>
    {
        public List<BaseViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<BaseViewModel> GetAllChangedAfter(DateTime selectedDate)
        {
            throw new NotImplementedException();
        }
        #region Ctors
        public SDSSampleDomain()
        { }
        #endregion

        #region Methods
        //public List<SDSBaseViewModel> GetAll()
        //{
        //    var storeActiveOnhands = StoreAdapter.Instance.GetAllStoreOnHands(DateTime.MinValue);
        //    return storeActiveOnhands;
        //}

        #endregion
    }
}
