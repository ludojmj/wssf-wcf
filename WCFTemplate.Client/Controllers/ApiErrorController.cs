using System.Web.Http;
using WCFTemplate.Client.Repositories;

namespace WCFTemplate.Client.Controllers
{
    public class ApiErrorController : ApiController
    {
        private readonly ITestRepo _repoTest;

        public ApiErrorController()
        {
            _repoTest = new TestRepo();
        }

        public ApiErrorController(ITestRepo repoTest)
        {
            _repoTest = repoTest;
        }

        public IHttpActionResult Get(bool techErr)
        {
            if (techErr)
            {
                _repoTest.DoTechnicalError();
            }
            else
            {
                _repoTest.DoBusinessError();
            }
            
            return Ok();
        }
    }
}
