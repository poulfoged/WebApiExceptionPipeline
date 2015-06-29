using System.Collections.Concurrent;
using System.Web.Http.Filters;

namespace WepApiExceptionPipeline
{
    public class PipelineExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ConcurrentBag<ExceptionFilterAttribute> filters = new ConcurrentBag<ExceptionFilterAttribute>();

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            foreach (var exceptionFilter in filters)
                exceptionFilter.OnException(actionExecutedContext);
        }

        public PipelineExceptionFilterAttribute Add(ExceptionFilterAttribute filter)
        {
            filters.Add(filter);
            return this;
        }
    }
}