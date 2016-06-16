using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Mvc;
using ClassLibrary1;
using WebApplication1.WWF;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    public class ClickArgument
    {
        public Guid TaskId { get; set; }
        public Guid ButtonId { get; set; }
        public string Model { get; set; }
    }
    

    [System.Web.Http.RoutePrefix("api/wwf")]
    public class WWFController : ApiController
    {
        [System.Web.Http.HttpPost, System.Web.Http.Route("start")]
        public UserTaskEntry Start()
        {
            Guid userTaskId;
            using (var db = new Db())
            {
                var wwfId = WWFManager.CreateWorkFlowExecAndSave(db);

                userTaskId = db.UserTasks.Single(u => u.WWFId == wwfId).Id;
            }

            return Get(userTaskId);
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("click")]
        public UserTaskEntry Click([FromBody]ClickArgument arg)
        {
            using (var db = new Db())
            {
                var userTask = db.UserTasks.Find(arg.TaskId);
                WWFManager.Click(db, userTask.WWFId, arg.ButtonId.ToString(), arg.Model);
            }

            return Get(arg.TaskId);
        }

        [System.Web.Http.HttpGet]
        public UserTaskEntry Get(Guid id)
        {
            using (var db = new Db())
            {
                return db.UserTasks.Include(u => u.Buttons).FirstOrDefault(u => u.Id == id);
            }
        }
    }
}
