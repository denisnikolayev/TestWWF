using System;

namespace WebApplication1.Services
{
    public static class RequestAnswerServices
    {
        public static void OpenWaitTest(Guid wwfId, string bookmark)
        {
            var client = new OpenWaitService.ServiceOpenWaitClient();

            client.SetRecordProcessRequestAsync(wwfId, bookmark);
        }
    }
}