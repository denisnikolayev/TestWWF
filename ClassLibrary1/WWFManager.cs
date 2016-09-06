using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Activities.Hosting;
using System.Activities.Persistence;
using System.Activities.Statements.Tracking;
using System.Activities.Tracking;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.DurableInstancing;
using System.Threading;
using System.Web;
using System.Xml.Linq;
using ClassLibrary1;

namespace WebApplication1.WWF
{

    public static class WWFManager
    {
        public static Guid CreateWorkFlowExecAndSave(Db db, Activity activity)
        {
            var id = Guid.Empty;
            var wf = new WorkflowApplication(activity)
            {
                InstanceStore = InstanceStore
            };
            wf.Extensions.Add(db);
            wf.Extensions.Add(new EntityFrameworkPersistenceIOParticipant(db));

            //TODO: ловим все ошибки и момент завершения работы wf
            Exception innerException = null;

            AutoResetEvent wait = new AutoResetEvent(false);
            wf.OnUnhandledException = (e) =>
            {
                innerException = e.UnhandledException;
                wait.Set();
                return UnhandledExceptionAction.Abort;
            };


            wf.PersistableIdle = (e) =>
            {
                id = wf.Id;
                return PersistableIdleAction.Unload;
            };

            wf.Completed = e =>
            {
              
            };
            wf.Unloaded = (e) => wait.Set();
            wf.Aborted = (e) =>
            {
                innerException = e.Reason;
                wait.Set();
            };

            // запускаем
            wf.Run();
            wait.WaitOne();

            if (innerException != null)
            {
                if (innerException is System.ComponentModel.DataAnnotations.ValidationException)
                {
                    throw innerException;
                }

                throw new Exception("Исключение в процессе (см. внутренее исключение)", innerException);
            }

            return id;
        }

        public static void Click(Db db, Activity activity, Guid id, string buttonId, string action)
        {
            var wf = new WorkflowApplication(activity)
            {
                InstanceStore = InstanceStore
            };

            wf.Extensions.Add(db);
            wf.Extensions.Add(new EntityFrameworkPersistenceIOParticipant(db));

            wf.Load(id);
        
            // ловим все ошибки и момент завершения работы wf
            Exception innerException = null;

            AutoResetEvent wait = new AutoResetEvent(false);
            wf.OnUnhandledException = (e) =>
            {
                innerException = e.UnhandledException;
                wait.Set();
                return UnhandledExceptionAction.Abort;
            };


            wf.PersistableIdle = (e) => PersistableIdleAction.Unload;

            wf.Completed = e =>
            {

            };
            wf.Unloaded = (e) => wait.Set();
            wf.Aborted = (e) =>
            {
                innerException = e.Reason;
                wait.Set();
            };

            // запускаем
            var resumeResult = wf.ResumeBookmark(buttonId, action);
            if (resumeResult != BookmarkResumptionResult.Success)
            {
                throw new ValidationException("Не удалось запустить рабочий процесс " + resumeResult.ToString());
            }
             
            wait.WaitOne();

            if (innerException != null)
            {
                if (innerException is System.ComponentModel.DataAnnotations.ValidationException)
                {
                    throw innerException;
                }

                if (innerException.InnerException is System.ComponentModel.DataAnnotations.ValidationException)
                {
                    throw innerException.InnerException;
                }

                throw new Exception("Исключение в процессе (см. внутренее исключение)", innerException);
            }
        }

        private static InstanceStore instanceStore;
        private static InstanceStore InstanceStore
        {
            get
            {
                if (instanceStore == null)
                {
                    //instanceStore = new SqlWorkflowInstanceStore("Data Source=.;Initial Catalog=TestWWF;Asynchronous Processing=True;User ID=Denis;Password=P@ssw0rd");

                    //instanceStore = new SqlWorkflowInstanceStore("Data Source=.;Initial Catalog=TestWWF;Asynchronous Processing=True;Integrated Security=SSPI");

                    instanceStore = new SqlWorkflowInstanceStore("Server=EPKZKARW0468\\SQLEXPRESS;Database=TestWWF;Trusted_Connection=True;");

                    InstanceHandle handle = instanceStore.CreateInstanceHandle();
                    InstanceView view = instanceStore.Execute(handle, new CreateWorkflowOwnerCommand(), TimeSpan.FromSeconds(30));

                    handle.Free();

                    instanceStore.DefaultInstanceOwner = view.InstanceOwner;
                }

                return instanceStore;
            }
        }

        public static void ContinueExecution(Db db, Activity activity, Guid id, string bookmark)
        {
            var wf = new WorkflowApplication(activity)
            {
                InstanceStore = InstanceStore
            };

            wf.Extensions.Add(db);
            wf.Extensions.Add(new EntityFrameworkPersistenceIOParticipant(db));

            wf.Load(id);

            // ловим все ошибки и момент завершения работы wf
            Exception innerException = null;

            AutoResetEvent wait = new AutoResetEvent(false);
            wf.OnUnhandledException = (e) =>
            {
                innerException = e.UnhandledException;
                wait.Set();
                return UnhandledExceptionAction.Abort;
            };


            wf.PersistableIdle = (e) => PersistableIdleAction.Unload;

            wf.Completed = e =>
            {
            };
            wf.Unloaded = (e) => wait.Set();
            wf.Aborted = (e) =>
            {
                innerException = e.Reason;
                wait.Set();
            };

            // запускаем
            var resumeResult = wf.ResumeBookmark(bookmark, new object());
            if (resumeResult != BookmarkResumptionResult.Success)
            {
                throw new ValidationException("Не удалось запустить рабочий процесс " + resumeResult.ToString());
            }

            wait.WaitOne();

            if (innerException != null)
            {
                if (innerException is System.ComponentModel.DataAnnotations.ValidationException)
                {
                    throw innerException;
                }

                if (innerException.InnerException is System.ComponentModel.DataAnnotations.ValidationException)
                {
                    throw innerException.InnerException;
                }

                throw new Exception("Исключение в процессе (см. внутренее исключение)", innerException);
            }
        }
    }


    public class EntityFrameworkPersistenceIOParticipant : PersistenceIOParticipant
    {
        private Db db;

        public EntityFrameworkPersistenceIOParticipant(Db db)
            : base(true, false)
        {
            this.db = db;
        }

        protected override void Abort()
        {

        }

        protected override IAsyncResult BeginOnSave(IDictionary<System.Xml.Linq.XName, object> readWriteValues, IDictionary<System.Xml.Linq.XName, object> writeOnlyValues, TimeSpan timeout, AsyncCallback callback, object state)
        {
            db.SaveChanges();

            return base.BeginOnSave(readWriteValues, writeOnlyValues, timeout, callback, state);
        }
    }
}