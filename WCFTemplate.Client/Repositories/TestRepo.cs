using System;
using WCFTemplate.Client.Shared;

namespace WCFTemplate.Client.Repositories
{
    public class TestRepo : ITestRepo
    {
        public void DoTechnicalError()
        {
            throw new NotImplementedException();
        }
        public void DoBusinessError()
        {
            throw new BusinessException("Wrong parameter in Api");
        }
    }
}