using NLog;
using System;
using System.Text;
using System.Web.Http;
using System.Net.Http;
using System.Collections;
using Microsoft.AspNet.Identity;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity.Owin;
using DeviceBaseSystem.WebApi.Infrastructure;
using Anatoli.Cloud.WebApi.Models;

namespace Anatoli.Cloud.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        protected static readonly Logger log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ModelFactory _modelFactory;
        private ApplicationUserManager _AppUserManager = null;
        private ApplicationRoleManager _AppRoleManager = null;

        protected ApplicationUserManager AppUserManager
        {
            get
            {
                return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        protected ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        public BaseApiController()
        {
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                    _modelFactory = new ModelFactory(Request, AppUserManager);

                return _modelFactory;
            }
        }

        protected IHttpActionResult GetErrorResult(ModelStateDictionary modelState)
        {
            return BadRequest(modelState);
        }

        protected IHttpActionResult GetErrorResult(string error)
        {
            if (error == null)

                return InternalServerError();

            ModelState.AddModelError(string.Empty, error);

            return BadRequest(ModelState);
        }

        protected IHttpActionResult GetErrorResult(Exception ex)
        {
            if (ex == null)
                return InternalServerError();

            var builder = new StringBuilder();

            WriteExceptionDetails(ex, builder, 0, ModelState);

            return BadRequest(ModelState);
        }

        public static void WriteExceptionDetails(Exception exception, StringBuilder builderToFill, int level, ModelStateDictionary modelState)
        {
            var indent = new string(' ', level);

            if (level > 0)
                builderToFill.AppendLine(indent + "=== INNER EXCEPTION ===");

            Action<string> append = (prop) =>
            {
                var propInfo = exception.GetType().GetProperty(prop);
                var val = propInfo.GetValue(exception);

                if (val != null)
                {
                    builderToFill.AppendFormat("{0}{1}: {2}{3}", indent, prop, val.ToString(), Environment.NewLine);
                    modelState.AddModelError(exception.Message, String.Format("{0}{1}: {2}{3}", indent, prop, val.ToString(), Environment.NewLine));
                }
            };

            append("Message");
            append("HResult");
            append("HelpLink");
            append("Source");
            append("StackTrace");
            append("TargetSite");

            foreach (DictionaryEntry de in exception.Data)
            {
                builderToFill.AppendFormat("{0} {1} = {2}{3}", indent, de.Key, de.Value, Environment.NewLine);
                modelState.AddModelError(exception.Message, String.Format("{0} {1} = {2}{3}", indent, de.Key, de.Value, Environment.NewLine));
            }

            if (exception.InnerException != null)
                WriteExceptionDetails(exception.InnerException, builderToFill, ++level, modelState);
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
                return InternalServerError();

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                    foreach (string error in result.Errors)
                        ModelState.AddModelError("", error);

                // No ModelState errors are available to send, so just return an empty BadRequest.
                if (ModelState.IsValid)
                    return BadRequest(ModelState);

                return BadRequest(ModelState);
            }

            return null;
        }

        protected void LogMessage(HttpRequestMessage request, string logMessage, Exception ex)
        {
            /*
            log.
            var message = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(logMessage))
                message.Append("").Append(logMessage + Environment.NewLine);

            if (record.Request != null)
            {
                if (record.Request.Method != null)
                    message.Append("Method: " + record.Request.Method + Environment.NewLine);

                if (record.Request.RequestUri != null)
                    message.Append("").Append("URL: " + record.Request.RequestUri + Environment.NewLine);

                if (record.Request.Headers != null && record.Request.Headers.Contains("Token") && record.Request.Headers.GetValues("Token") != null && record.Request.Headers.GetValues("Token").FirstOrDefault() != null)
                    message.Append("").Append("Token: " + record.Request.Headers.GetValues("Token").FirstOrDefault() + Environment.NewLine);
            }

            if (!string.IsNullOrWhiteSpace(record.Category))
                message.Append("").Append(record.Category);

            if (!string.IsNullOrWhiteSpace(record.Operator))
                message.Append(" ").Append(record.Operator).Append(" ").Append(record.Operation);

            if (record.Exception != null && !string.IsNullOrWhiteSpace(record.Exception.GetBaseException().Message))
            {
                var exceptionType = record.Exception.GetType();
                message.Append(Environment.NewLine);
                message.Append("").Append("Error: " + record.Exception.GetBaseException().Message + Environment.NewLine);
            }

            Logger[record.Level](Convert.ToString(message) + Environment.NewLine);
            */
        }
    }
}
