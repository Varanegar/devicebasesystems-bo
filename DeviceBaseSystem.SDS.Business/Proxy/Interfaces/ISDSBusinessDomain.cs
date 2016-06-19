using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Anatoli.SDS.Business.Proxy.Interfaces;
using Anatoli.SDS.DataAccess.DataAdapter;

namespace Anatoli.SDS.Business
{
    public interface ISDSBusinessDomain<TSource, TOut>
        where TSource : class, new()
        where TOut : class, new()
    {
        List<TOut> GetAll();
        List<TOut> GetAllChangedAfter(DateTime selectedDate);
    }
}
