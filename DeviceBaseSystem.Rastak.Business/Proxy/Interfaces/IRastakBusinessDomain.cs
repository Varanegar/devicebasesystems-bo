using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Anatoli.Rastak.Business.Proxy.Interfaces;
using Anatoli.Rastak.DataAccess.DataAdapter;

namespace Anatoli.Rastak.Business
{
    public interface IRastakBusinessDomain<TSource, TOut>
        where TSource : class, new()
        where TOut : class, new()
    {
        List<TOut> GetAll();
        List<TOut> GetAllChangedAfter(DateTime selectedDate);
    }
}
