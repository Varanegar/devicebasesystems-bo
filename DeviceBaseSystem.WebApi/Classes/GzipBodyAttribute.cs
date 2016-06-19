//using System;
//using System.IO;
//using System.Net.Http;
//using System.Web.Http;
//using Newtonsoft.Json;
//using System.Threading;
//using System.IO.Compression;
//using System.Threading.Tasks;
//using System.Net.Http.Headers;
//using System.Web.Http.Metadata;
//using System.Web.Http.Controllers;

namespace DeviceBaseSystem.WebApi.Classes
{
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    //public sealed class GzipBodyAttribute : ParameterBindingAttribute
    //{
    //    public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
    //    {
    //        if (parameter == null)
    //            throw new ArgumentException("Invalid parameter");

    //        return new GzipBodyParameterBinding(parameter);
    //    }
    //}

    //public class EmptyTask
    //{
    //    public static Task Start()
    //    {
    //        var taskSource = new TaskCompletionSource<AsyncVoid>();

    //        taskSource.SetResult(default(AsyncVoid));

    //        return taskSource.Task as Task;
    //    }

    //    private struct AsyncVoid { }
    //}

    //public class GzipBodyParameterBinding : HttpParameterBinding
    //{
    //    public GzipBodyParameterBinding(HttpParameterDescriptor descriptor) : base(descriptor) { }

    //    public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider,
    //                                             HttpActionContext actionContext,
    //                                             CancellationToken cancellationToken)
    //    {
    //        var binding = actionContext.ActionDescriptor.ActionBinding;

    //        if (actionContext.Request.Method == HttpMethod.Get ||
    //           !actionContext.Request.Headers.AcceptEncoding.Contains(new StringWithQualityHeaderValue("gzip")))
    //            return EmptyTask.Start();

    //        return actionContext.Request.Content.ReadAsStreamAsync().ContinueWith((task) =>
    //        {
    //            var s = task.Result;
    //            using (var decompressed = new GZipStream(s, CompressionMode.Decompress))
    //            {
    //                using (var sr = new StreamReader(decompressed))
    //                {
    //                    var jsonResult = sr.ReadToEnd();

    //                    var type = binding.ParameterBindings[0].Descriptor.ParameterType;

    //                    var model = JsonConvert.DeserializeObject(jsonResult, type);

    //                    SetValue(actionContext, model);
    //                }
    //            }
    //        });
    //    }
    //}
}