using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ClassLibrary1;
using WebApplication1.Controllers;
using WebApplication1.WWF;

namespace WebApplication1.WebAplicationService
{
    public class WebAplicationService : IWebAplicationService
    {
        public void ApproveFineshedProcessBywwfId(Guid wwfIdGuid, string bookmark)
        {
            using (var db = new Db())
            {
                var approvalProcessEntry = db.ApprovalProcess.First(x => x.WwfId == wwfIdGuid && x.Bookmark == bookmark);
                approvalProcessEntry.QueueName = "Подтвержденные";
                approvalProcessEntry.Approve = true;
                db.ApprovalProcess.AddOrUpdate(approvalProcessEntry);
                WWFManager.ContinueExecution(db, new RequestCardWorkFlow(), wwfIdGuid, bookmark);
            }
        }
    }
}
