using Anatoli.PMC.DataAccess.Helpers.Entity;
using Anatoli.PMC.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.DataAccess.Helpers
{
    public class StoreConfigHeler
    {
        protected static readonly Logger log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());
        private static PMCStoreConfigEntity currentConfig = null;
        public List<PMCStoreConfigEntity> AllStoreConfigs { get; private set; }

        private static StoreConfigHeler instance = null;
        public static StoreConfigHeler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StoreConfigHeler();
                }
                return instance;
            }
        }
        StoreConfigHeler()
        {
            using (DataContext context = new DataContext())
            {
                DataObject<PMCStoreConfigEntity> configDataObject = new DataObject<PMCStoreConfigEntity>("vwCenterCloud");
                AllStoreConfigs= configDataObject.Select.All("where isReal=1").ToList();
            }
        }

        public PMCStoreConfigEntity CurrentConfig
        {
            get
            {
                using (DataContext context = new DataContext())
                {
                    if (currentConfig == null)
                    {
                        DataObject<PMCStoreConfigEntity> configDataObject = new DataObject<PMCStoreConfigEntity>("Center");
                        currentConfig = configDataObject.Select.First("where centerId in (select top 1 centerid from CenterSetting)");

                    }
                    currentConfig.FiscalYearId = context.GetValue<int>(DBQuery.Instance.GetFiscalYearId());
                }

                return currentConfig;
            }
        }

        public PMCStoreConfigEntity GetStoreConfig(string storeUniqueId)
        {
            if(storeUniqueId == "all")
                return AllStoreConfigs.Find(p => p.CenterId  == 1);
            else
                return AllStoreConfigs.Find(p => p.UniqueId.ToLower() == storeUniqueId.ToLower());
        }

        public PMCStoreConfigEntity GetStoreConfig(int storeId)
        {
            var config = AllStoreConfigs.Find(p => p.CenterId == storeId);
            using (var context = new DataContext())
            {
                config.FiscalYearId = context.GetValue<int>(DBQuery.Instance.GetFiscalYearId());
            }
            return config;
        }
    }
}
