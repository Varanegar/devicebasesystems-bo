using Anatoli.Rastak.ViewModels;
using Anatoli.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Rastak.Business.Proxy.Interfaces
{
    public class RastakBusinessDomain<TSource, TOut>
        where TSource : RastakBaseViewModel, new()
        where TOut : BaseViewModel, new()
    {
        protected static readonly Logger log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());

    }
}
