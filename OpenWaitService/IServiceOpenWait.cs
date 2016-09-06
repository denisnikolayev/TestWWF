using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace OpenWaitService
{
    [ServiceContract]
    public interface IServiceOpenWait
    {
        [OperationContract]
        void SetRecordProcessRequest(Guid wwfId, string bookmark);
    }
}
