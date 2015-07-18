using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace WepApiExceptionPipeline
{
    public class PipelineExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ConcurrentBag<ExceptionFilterAttribute> filters = new ConcurrentBag<ExceptionFilterAttribute>();

        public override async Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            foreach (var exceptionFilter in filters)
                await exceptionFilter.OnExceptionAsync(actionExecutedContext, cancellationToken);
        }

        public PipelineExceptionFilterAttribute Add(ExceptionFilterAttribute filter)
        {
            filters.Add(filter);
            return this;
        }
    }
}