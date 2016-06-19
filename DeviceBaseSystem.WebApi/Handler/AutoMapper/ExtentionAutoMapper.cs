using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anatoli.DataAccess.Models.PersonnelAcitvity;
using Anatoli.ViewModels.PersonnelAcitvityModel;
using AutoMapper;

namespace Anatoli.Cloud.WebApi.Handler.AutoMapper
{
    public static class ExtentionAutoMapper
    {
        public static PersonnelDailyActivityEvent ToModel(this PersonnelDailyActivityEventViewModel view)
        {
            return Mapper.Map<PersonnelDailyActivityEvent>(view);
        }
        public static PersonnelDailyActivityPoint ToPointModel(this PersonnelDailyActivityEventViewModel view)
        {
            return Mapper.Map<PersonnelDailyActivityPoint>(view);
        }

        public static PersonnelDailyActivityPoint ToModel(this PersonnelDailyActivityPointViewModel view)
        {
            return Mapper.Map<PersonnelDailyActivityPoint>(view);
        }
    }
}