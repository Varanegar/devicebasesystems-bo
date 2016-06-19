using System;
using System.Net;
using System.Web;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
//using Anatoli.Business.Domain.Authorization;

namespace DeviceBaseSystem.WebApi.Classes
{
    public class AnatoliLoggerAttribute : ActionFilterAttribute
    {
        //protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //public override void OnActionExecuting(HttpActionContext filterContext)
        //{
        //    //filterContext.Request
        //    "Controller : " + filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine + "Action : " + filterContext.ActionDescriptor.ActionName, "JSON", filterContext.ActionArguments

        //    GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
        //    var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
        //    trace.Info(filterContext.Request, "Controller : " + filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine + "Action : " + filterContext.ActionDescriptor.ActionName, "JSON", filterContext.ActionArguments);
        //}
    }
}