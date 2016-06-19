using AutoMapper;
using Anatoli.ViewModels.Order;
using Anatoli.DataAccess.Models;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.StoreModels;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.CustomerModels;


namespace Anatoli.Cloud.WebApi.Handler
{
    public static class ConfigAutoMapperHelper
    {
        public static void Config()
        {
            Mapper.CreateMap<FiscalYear, FiscalYearViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<CityRegion, CityRegionViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ParentUniqueIdString, opt => opt.MapFrom(src => src.CityRegion2Id.ToString())).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<Manufacture, ManufactureViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<Supplier, SupplierViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<BaseType, BaseTypeViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<BaseValue, BaseValueViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<BasketItem, BasketItemViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<Basket, BasketViewModel>().ForMember(p => p.BasketTypeValueId, opt => opt.MapFrom(src => src.BasketTypeValueGuid)).ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<ItemImage, ItemImageViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.BaseDataId, opt => opt.MapFrom(src => src.TokenId)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<ReorderCalcType, ReorderCalcTypeViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());

            Mapper.CreateMap<CharType, CharTypeViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<CharGroup, CharGroupViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<CharValue, CharValueViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());

            Mapper.CreateMap<ProductGroup, ProductGroupViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ParentUniqueIdString, opt => opt.MapFrom(src => src.ProductGroup2Id.ToString())).ForMember(p => p.ParentUniqueId, opt => opt.MapFrom(src => src.ProductGroup2Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<Product, ProductViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductRate, ProductRateViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductTag, ProductTagViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductTagValue, ProductTagValueViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductType, ProductViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductPicture, ProductPictureViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<MainProductGroup, MainProductGroupViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ParentUniqueIdString, opt => opt.MapFrom(src => src.ProductGroup2Id.ToString())).ForMember(p => p.ParentUniqueId, opt => opt.MapFrom(src => src.ProductGroup2Id)).ForMember(p => p.ID, opt => opt.Ignore());

            Mapper.CreateMap<IncompletePurchaseOrderLineItem, IncompletePurchaseOrderLineItemViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<IncompletePurchaseOrder, IncompletePurchaseOrderViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ShipAddressId, opt => opt.MapFrom(src => src.CustomerShipAddressId)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<PurchaseOrderStatusHistory, PurchaseOrderStatusHistoryViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<PurchaseOrder, PurchaseOrderViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());

            Mapper.CreateMap<CustomerShipAddress, CustomerShipAddressViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<Customer, CustomerViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());


            Mapper.CreateMap<Store, StoreViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StoreCalendar, StoreCalendarViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StoreActiveOnhand, StoreActiveOnhandViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ProductGuid, opt => opt.MapFrom(src => src.ProductId)).ForMember(p => p.StoreGuid, opt => opt.MapFrom(src => src.StoreId)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StoreActivePriceList, StoreActivePriceListViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ProductGuid, opt => opt.MapFrom(src => src.ProductId)).ForMember(p => p.StoreGuid, opt => opt.MapFrom(src => src.StoreId)).ForMember(p => p.ID, opt => opt.Ignore());

            Mapper.CreateMap<Stock, StockViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProduct, StockProductViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockHistoryOnHand, StockHistoryOnHandViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockOnHandSync, StockOnHandSyncViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockType, StockTypeViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockActiveOnHand, StockActiveOnHandViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());

            Mapper.CreateMap<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestProduct, StockProductRequestProductViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestRuleType, StockProductRequestRuleTypeViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestRule, StockProductRequestRuleViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestStatus, StockProductRequestStatusViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestSupplyType, StockProductRequestSupplyTypeViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestType, StockProductRequestTypeViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequest, StockProductRequestViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());

        }
    }
}