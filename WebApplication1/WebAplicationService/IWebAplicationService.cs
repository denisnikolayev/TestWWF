using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebApplication1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWebAplicationService" in both code and config file together.
    [ServiceContract]
    public interface IWebAplicationService
    {
        [OperationContract]
        void ApproveFineshedProcessBywwfId(Guid wwfId, string bookmark);
    }
}
