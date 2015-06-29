using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WepApiExceptionPipeline.WebDummy
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var configuration = GlobalConfiguration.Configuration;

            configuration.Filters.Add(
                new PipelineExceptionFilterAttribute()
                    .Add(new ExceptionTranslatorFilterAttribute()
                        .Register<UserNotLoggedInException>((exception, request) => new HttpResponseMessage(HttpStatusCode.Forbidden) { ReasonPhrase = "This is forbidden" })
                        .Register<NotImplementedException>((exception, request) => new HttpResponseMessage(HttpStatusCode.NotImplemented) { ReasonPhrase = "Future feature" })
                    )
                    .Add(new ExceptionLoggerFilterAttribute()
                        .LogTo((exception, request) => Debug.WriteLine("EXCEPTION: " + request.RequestUri + " resulted in exception " + exception))));

            configuration.MapHttpAttributeRoutes();
            configuration.EnsureInitialized();
        }
    }
}
