using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Business.Helpers
{
    public static class WebApiURIHelper
    {
        public static readonly string SaveSupplierURI = "/api/gateway/base/supplier/save";
        public static readonly string SaveManufactureURI = "/api/gateway/base/manufacture/save";
        public static readonly string SaveCityRegionURI = "/api/gateway/base/region/save";
        public static readonly string SaveCharGroupURI = "/api/gateway/product/chargroups/save";
        public static readonly string SaveCharTypeURI = "/api/gateway/product/chartypes/save";
        public static readonly string SaveProductURI = "/api/gateway/product/save";
        public static readonly string SaveProductGroupURI = "/api/gateway/product/productgroups/save";
        public static readonly string SaveStoreURI = "/api/gateway/store/save";
        public static readonly string SaveStorePriceListURI = "/api/gateway/store/storepricelist/save";
        public static readonly string SaveStoreOnHandURI = "/api/gateway/store/storeOnhand/save";
        public static readonly string SaveImageURI = "/api/imageManager/Save";

        public static readonly string GetStoreOnHandLocalURI = "/api/gateway/store/StoreOnhandById/local";
        public static readonly string GetPoByCustomerIdLocalURI = "/api/gateway/purchaseorder/bycustomerid/local";
        public static readonly string GetPoStatusHistoryByPoIdLocalURI = "/api/gateway/purchaseorder/statushistory/local";
        public static readonly string GetPoLineItemsByPoIdLocalURI = "/api/gateway/purchaseorder/lineitem/local";
        public static readonly string SaveOrderLocalURI = "/api/gateway/purchaseorder/local/create";
        public static readonly string CalcPromoLocalURI = "/api/gateway/purchaseorder/local/calcpromo";
    }
}
