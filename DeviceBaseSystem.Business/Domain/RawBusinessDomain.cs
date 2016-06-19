using DeviceBaseSystem.DataAccess;
using DeviceBaseSystem.DataAccess.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Business.Domain
{
    public class RawBusinessDomain<TMainRepository> where TMainRepository : class
    {
        #region Properties
        protected static readonly Logger Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());

        public IPrincipalRepository PrincipalRepository
        {
            get;
            set;
        }

        public Guid ApplicationOwnerKey
        {
            get;
            protected set;
        }

        public Guid DataOwnerKey
        {
            get;
            protected set;
        }

        public Guid DataOwnerCenterKey
        {
            get;
            protected set;
        }

        public bool GetRemovedData
        {
            get;
            protected set;
        }

        public virtual TMainRepository MainRepository
        {
            get;
            set;
        }

        public AnatoliDbContext DBContext
        {
            get;
            set;
        }

        #endregion

        public RawBusinessDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
        {
            ApplicationOwnerKey = applicationOwnerKey;
            DataOwnerKey = dataOwnerKey;
            DataOwnerCenterKey = dataOwnerCenterKey;
        }
    }
}
