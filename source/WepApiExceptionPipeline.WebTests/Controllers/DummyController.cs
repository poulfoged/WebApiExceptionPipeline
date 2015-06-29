using System;
using System.Web.Http;

namespace WepApiExceptionPipeline.WebDummy.Controllers
{
    [RoutePrefix("")]
    public class DummyController : ApiController
    {
        [HttpGet, Route("")]
        public string Get()
        {
            throw new UserNotLoggedInException();
        }

        [HttpGet, Route("not-implemented")]
        public string NotImplemented()
        {
            throw new NotImplementedException();
        }
    }
}
