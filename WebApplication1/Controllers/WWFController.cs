using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using ClassLibrary1;
using WebApplication1.WWF;
using System.Data.Entity;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ClickArgument
    {
        public Guid TaskId { get; set; }
        public Guid ButtonId { get; set; }
        public string Model { get; set; }
    }


    public class QueueInfo
    {
        public string Name { get; set; }

        public IEnumerable<TaskInfo> Tasks { get; set; }
    }

    public class TaskInfo
    {
        public string Caption { get; set; }
        public Guid Id { get; set; }
        public int Number { get; set; }
    }

    public class TaskCanceledInfo
    {
        public string Name { get; set; }
        public IEnumerable<InfoCanceled> InfoCanceled { get; set; }
    }

    public class InfoCanceled
    {
        public string Caption { get; set; }
        public string CommentText { get; set; }
    }

    public class TaskInfoApprove
    {
        public string Name { get; set; }
        public IEnumerable<InfoApprove> InfoText { get; set; }
    }

    public class InfoApprove
    {
        public string Info { get; set; }
    }

    [RoutePrefix("api/wwf")]
    public class WWFController : ApiController
    {
        [HttpPost, Route("start")]
        public UserTaskEntry Start()
        {
            Guid userTaskId;
            using (var db = new Db())
            {
                var wwfId = WWFManager.CreateWorkFlowExecAndSave(db, new RequestCardWorkFlow());

                userTaskId = db.UserTasks.Single(u => u.WWFId == wwfId).Id;
            }

            return Get(userTaskId);
        }

        [HttpPost, Route("click")]
        public UserTaskEntry Click([FromBody]ClickArgument arg)
        {
            using (var db = new Db())
            {
                var userTask = db.UserTasks.Find(arg.TaskId);
                WWFManager.Click(db, new RequestCardWorkFlow(), userTask.WWFId, arg.ButtonId.ToString(), arg.Model);
            }

            return Get(arg.TaskId);
        }

        [HttpGet]
        public UserTaskEntry Get(Guid id)
        {
            using (var db = new Db())
            {
                return db.UserTasks.Include(u => u.Buttons).FirstOrDefault(u => u.Id == id);
            }
        }

        [HttpGet, Route("queues")]
        public QueueInfo[] Queues()
        {
            using (var db = new Db())
            {
                return db.UserTasks.GroupBy(u => u.QueueName)
                    .Select(g => new QueueInfo()
                    {
                        Name = g.Key,
                        Tasks = g.Select(t => new TaskInfo() {Caption = t.Caption, Id = t.Id, Number = t.Number})
                    }).ToArray();
            }
        }

        [HttpGet, Route("taskCanceled")]
        public TaskCanceledInfo[] GetTaskCanceled()
        {
            using (var db = new Db())
            {
                return db.TaskCanceledWithComment.GroupBy(u => u.QueueName)
                    .Select(g => new TaskCanceledInfo()
                    {
                        Name = g.Key,
                        InfoCanceled = g.Select(t => new InfoCanceled() { Caption = t.Caption, CommentText = t.TextComment})
                    }).ToArray();
            }
        }

        [HttpGet, Route("taskApprove")]
        public TaskInfoApprove[] GetTaskApprove()
        {
            using (var db = new Db())
            {
                return db.ApprovalProcess.GroupBy(u => u.QueueName)
                    .Select(g => new TaskInfoApprove()
                    {
                        Name = g.Key,
                        InfoText = g.Select(t => new InfoApprove() { Info = t.Info })
                    }).ToArray();
            }
        }
    }
}
