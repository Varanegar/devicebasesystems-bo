using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Business.Helpers
{
    [DataContract]
    public class ApiResponse
    {
        [DataMember]
        public string Version { get; set;  }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public HttpError HttpErrorMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string HttpErrorMessageString { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public object Result { get; set; }

        public ApiResponse(HttpStatusCode statusCode, string version = "1.0.0.0", object result = null, HttpError httpError = null, string httpErrorString = null, string errorMessage = null)
        {
            StatusCode = (int)statusCode;
            Result = result;
            ErrorMessage = errorMessage;
            HttpErrorMessage = httpError;
            Version = version;
            HttpErrorMessageString = httpErrorString;
        }
    }
}
