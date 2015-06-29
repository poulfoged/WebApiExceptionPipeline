using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace WepApiExceptionPipeline
{
    public class ExceptionLoggerFilterAttribute : ExceptionFilterAttribute
    {
        private Action<Exception, HttpRequestMessage> action;

        public override async Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (actionExecutedContext.Exception != null)
                action.Invoke(actionExecutedContext.Exception, actionExecutedContext.Request);
        }

        public ExceptionLoggerFilterAttribute LogTo(Action<Exception, HttpRequestMessage> action)
        {
            this.action = action;
            return this;
        }
    }
}