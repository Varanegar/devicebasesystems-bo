using Anatoli.ViewModels.DeviceBaseSystem;
using AutoMapper;
using DeviceBaseSystem.DataAccess.Models;
using System;

namespace DeviceBaseSystem.WebApi.Handler
{
    public static class ConfigDefaultAutoMapperHelper
    {
        public static void Config()
        {
            ConfigModelToViewModel();
            ConfigViewModelToModel();
        }
        private static void ConfigModelToViewModel()
        {
            Mapper.CreateMap<DeviceModel, DeviceModelViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore()).ForMember(p => p.brandName, opt => opt.MapFrom(src => src.Brand.BrandName));
            Mapper.CreateMap<Brand, BrandViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<Company, CompanyViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<CompanyDevice, CompanyDeviceViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore()).ForMember(p => p.DeviceModelName, opt => opt.MapFrom(src => src.DeviceModel.DeviceName)); 
        }

        private static void ConfigViewModelToModel()
        {
            Mapper.CreateMap<DeviceModelViewModel, DeviceModel>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<BrandViewModel, Brand>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<CompanyViewModel, Company>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<CompanyDeviceViewModel, CompanyDevice>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());

        }
        private static Guid? ConvertNullableStringToGuid(string data)
        {
            var guid = Guid.Empty;
            Guid.TryParse(data, out guid);
            return guid;
        }

    }
}