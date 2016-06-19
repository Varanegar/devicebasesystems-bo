using Anatoli.DataAccess.Models;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.Order;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.StoreModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anatoli.Cloud.WebApi.Handler
{
    public static class ConfigureRastakAutoMapper
    {
        public static void Config()
        {
            ConfigModelToViewModel();
            ConfigViewModelToModel();
        }
        private static void ConfigModelToViewModel()
        {
        }

        private static void ConfigViewModelToModel()
        {
        }

        private static Guid? ConvertNullableStringToGuid(string data)
        {
            var guid = Guid.Empty;
            Guid.TryParse(data, out guid);
            return guid;
        }

    }
}