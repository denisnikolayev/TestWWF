using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace OpenWaitService
{
    public class ServiceOpenWait : IServiceOpenWait
    {
        public void SetRecordProcessRequest(Guid wwfId, string bookmark)
        {
            //просто залипаем, а по идее какие то действия от сервиса ждем
            Thread.Sleep(20000);
           
            var client = new WebApplicationService.WebAplicationServiceClient();
            client.ApproveFineshedProcessBywwfIdAsync(wwfId, bookmark);
        }
    }
}

