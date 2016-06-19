using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    public static class ExtentionDMCAutoMapper
    {
        #region RegionArea
        public static RegionAreaViewModel ToViewModel(this  DMCVisitTemplatePathViewModel view)
        {
            return Mapper.Map<RegionAreaViewModel>(view);
        }
        public static DMCVisitTemplatePathViewModel ToDMCViewModel(this RegionAreaViewModel view)
        {
            return Mapper.Map<DMCVisitTemplatePathViewModel>(view);
        }
        public static SelectListItemViewModel ToSelectedItem(this  DMCVisitTemplatePathViewModel view)
        {
            return Mapper.Map<SelectListItemViewModel>(view);
        }
        #endregion

        #region Customer
        public static CustomerComboViewModel ToComboViewModel(this  DMCCustomerViewModel view)
        {
            return Mapper.Map<CustomerComboViewModel>(view);
        }
        #endregion

        #region AreaCustomer
        public static DMCRegionAreaCustomerViewModel ToDMCViewModel(this RegionAreaCustomerViewModel view)
        {
            return Mapper.Map<DMCRegionAreaCustomerViewModel>(view);
        }

        public static RegionAreaCustomerViewModel ToViewModel(this DMCRegionAreaCustomerViewModel view)
        {
            return Mapper.Map<RegionAreaCustomerViewModel>(view);
        }
        public static DMCCustomerPointViewModel ToDMCViewModel(this CustomerPointViewModel view)
        {
            return Mapper.Map<DMCCustomerPointViewModel>(view);
        }

        public static CustomerPointViewModel ToViewModel(this DMCCustomerPointViewModel view)
        {
            return Mapper.Map<CustomerPointViewModel>(view);
        }
        #endregion

        #region AreaPoint
        public static DMCRegionAreaPointViewModel ToDMCViewModel(this RegionAreaPointViewModel view)
        {
            return Mapper.Map<DMCRegionAreaPointViewModel>(view);
        }

        #endregion

        #region Gis
        public static DMCPolyViewModel ToDMCViewModel(this PolyViewModel view)
        {
            return Mapper.Map<DMCPolyViewModel>(view);
        }

        public static PolyViewModel ToViewModel(this DMCPolyViewModel view)
        {
            return Mapper.Map<PolyViewModel>(view);
        }

        public static DMCPointViewModel ToDMCViewModel(this PointViewModel view)
        {
            return Mapper.Map<DMCPointViewModel>(view);
        }

        public static PointViewModel ToViewModel(this DMCPointViewModel view)
        {
            return Mapper.Map<PointViewModel>(view);
        }


        
        #endregion

        #region report
        public static DMCProductValueReportFilterModel ToDMCProductValueReportFilterViewModel(this ProductReportRequestModel view)
        {
            return Mapper.Map<DMCProductValueReportFilterModel>(view);
        }

        public static DMCProductReportFilterModel ToDMCProductReportFilterViewModel(this ProductReportRequestModel view)
        {
            return Mapper.Map<DMCProductReportFilterModel>(view);
        }
        #endregion

    }
}