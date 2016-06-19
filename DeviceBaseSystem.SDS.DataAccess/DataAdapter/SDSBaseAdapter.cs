using Anatoli.SDS.DataAccess.Helpers;
using Anatoli.ViewModels.BaseModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.SDS.DataAccess.DataAdapter
{
    public abstract class SDSBaseAdapter
    {

        protected static readonly Logger log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());

        protected DataContext GetDataContext(Transaction tran = Transaction.Begin)
        {
            return new DataContext("SDSConnectionString", tran);
        }

    }
}
