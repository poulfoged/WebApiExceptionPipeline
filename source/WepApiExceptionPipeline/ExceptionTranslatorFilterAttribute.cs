using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Web.Http.Filters;
using WepApiExceptionPipeline.Extensions;

namespace WepApiExceptionPipeline
{
    public class ExceptionTranslatorFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ConcurrentDictionary<Type, Func<Exception, HttpRequestMessage, HttpResponseMessage>> registrations = new ConcurrentDictionary<Type, Func<Exception, HttpRequestMessage, HttpResponseMessage>>();

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null) 
                return;
           
            var type = actionExecutedContext.Exception.GetType();
            if (!registrations.ContainsKey(type))
                return;

            var handler = registrations[type];

            actionExecutedContext.Response = handler.Invoke(actionExecutedContext.Exception, actionExecutedContext.Request);
        }

        public ExceptionTranslatorFilterAttribute Register<TException>(Func<Exception, HttpRequestMessage, HttpResponseMessage> action)
        {
            registrations.TryReplace(typeof (TException), action);
            return this;
        }
    }
}
