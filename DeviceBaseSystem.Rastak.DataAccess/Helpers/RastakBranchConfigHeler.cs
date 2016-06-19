using Anatoli.Rastak.DataAccess.Helpers.Entity;
using Anatoli.Rastak.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.Rastak.DataAccess.Helpers
{
    public class RastakBranchConfigHeler
    {
        protected static readonly Logger log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());
        private static RastakBranchConfigEntity currentConfig = null;
        public List<RastakBranchConfigEntity> AllStoreConfigs { get; private set; }

        private static RastakBranchConfigHeler instance = null;
        public static RastakBranchConfigHeler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RastakBranchConfigHeler();
                }
                return instance;
            }
        }
        RastakBranchConfigHeler()
        {
            using (DataContext context = new DataContext())
            {
                DataObject<RastakBranchConfigEntity> configDataObject = new DataObject<RastakBranchConfigEntity>("Center");
                AllStoreConfigs= configDataObject.Select.All("where isReal=1").ToList();
            }
        }

        public RastakBranchConfigEntity CurrentConfig
        {
            get
            {
                using (DataContext context = new DataContext())
                {
                    if (currentConfig == null)
                    {
                        DataObject<RastakBranchConfigEntity> configDataObject = new DataObject<RastakBranchConfigEntity>("Center");
                        currentConfig = configDataObject.Select.First("where centerId in (select top 1 centerid from CenterSetting)");

                    }
                    currentConfig.FiscalYearId = context.GetValue<int>(RastakDBQuery.Instance.GetFiscalYearId());
                }

                return currentConfig;
            }
        }

        public RastakBranchConfigEntity GetStoreConfig(string storeUniqueId)
        {
            var config = AllStoreConfigs.Find(p => p.UniqueId.ToLower() == storeUniqueId.ToLower());
            config.FiscalYearId = new DataContext().GetValue<int>(RastakDBQuery.Instance.GetFiscalYearId());
            return config;
        }

        public RastakBranchConfigEntity GetStoreConfig(int storeId)
        {
            var config = AllStoreConfigs.Find(p => p.CenterId == storeId);
            config.FiscalYearId = new DataContext().GetValue<int>(RastakDBQuery.Instance.GetFiscalYearId());
            return config;
        }
    }
}
