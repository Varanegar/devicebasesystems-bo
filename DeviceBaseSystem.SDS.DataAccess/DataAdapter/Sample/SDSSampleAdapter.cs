using System.Collections.Generic;
using System.Linq;
using Thunderstruck;

namespace Anatoli.SDS.DataAccess.DataAdapter.Gis
{
    public class SDSSampleAdapter : SDSBaseAdapter
    {
        #region ctor
        private static SDSSampleAdapter instance = null;
        public static SDSSampleAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SDSSampleAdapter();
                }
                return instance;
            }
        }
        private SDSSampleAdapter() { }
        #endregion

        #region method

        #endregion

        #region tools
        #endregion
    }
}
