using Anatoli.PMC.DataAccess.Helpers;
using Anatoli.ViewModels.BaseModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.DataAccess.DataAdapter
{
    public abstract class BaseAdapter
    {
        protected static readonly Logger log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());

    }
}
