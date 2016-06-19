using System;
using Anatoli.DMC.ViewModels.Area;
using Anatoli.DMC.ViewModels.Base;
using Anatoli.DMC.ViewModels.Gis;
using Anatoli.DMC.ViewModels.Report;
using Anatoli.ViewModels.CommonModels;
using Anatoli.ViewModels.RequestModel;
using Anatoli.ViewModels.VnGisModels;
using AutoMapper;

namespace Anatoli.Cloud.WebApi.Handler.AutoMapper
{
    public static class ConfigDMCAutoMapperHelper
    {
        public static void Config()
        {
            ConfigModelToViewModel();
            ConfigViewModelToModel();
            ConfigDMCViewModelToViewModel();
            ConfigViewModelToDMCViewModel();
        }

        private static void ConfigModelToViewModel()
        {
        }

        private static void ConfigViewModelToModel()
        {
        }

        private static void ConfigDMCViewModelToViewModel()
        {
            Mapper.CreateMap<DMCVisitTemplatePathViewModel, RegionAreaViewModel>()
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.PathTitle))
                .ForMember(dest => dest.IsRemoved, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(dest => dest.Priority, opt => opt.Ignore())
                .ForMember(dest => dest.RegionAreaLevelTypeId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DataOwnerCenterId, opt => opt.Ignore())
                .ForMember(dest => dest.DataOwnerId, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.NLevel, opt => opt.Ignore());
            Mapper.CreateMap<DMCVisitTemplatePathViewModel, SelectListItemViewModel>()
                        .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.PathTitle));
            Mapper.CreateMap<DMCRegionAreaCustomerViewModel, RegionAreaCustomerViewModel>();
            Mapper.CreateMap<DMCPolyViewModel, PolyViewModel>();
            Mapper.CreateMap<DMCPointViewModel, PointViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UniqueId));
            Mapper.CreateMap<DMCCustomerViewModel, CustomerComboViewModel>()
                .ForMember(dest => dest.hasLatLng, opt => opt.MapFrom(src => src.Latitude + src.Longitude > 0))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.CustomerName + ' '+ src.CustomerCode));


        }

        private static void ConfigViewModelToDMCViewModel()
        {
            Mapper.CreateMap<RegionAreaPointViewModel, DMCRegionAreaPointViewModel>()
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Lng))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Lat))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Pr))
                .ForMember(dest => dest.CustomerUniqueId, opt => opt.MapFrom(src => src.CstId));

            Mapper.CreateMap<ProductReportRequestModel, DMCProductReportFilterModel>();
            Mapper.CreateMap<ProductReportRequestModel, DMCProductValueReportFilterModel>();

            Mapper.CreateMap<PolyViewModel, DMCPolyViewModel>();
            Mapper.CreateMap<PointViewModel, DMCPointViewModel>();
            Mapper.CreateMap<CustomerPointViewModel, DMCCustomerPointViewModel>()
                .ForMember(dest => dest.CustomerUniqueId, opt => opt.MapFrom(src => src.UniqueId))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Lng))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Lat));
            
        }

        private static Guid? ConvertNullableStringToGuid(string data)
        {
            var guid = Guid.Empty;
            Guid.TryParse(data, out guid);
            return guid;
        }

    }
}