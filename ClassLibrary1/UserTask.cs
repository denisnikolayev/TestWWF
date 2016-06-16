using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.Statements;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Workflow.ComponentModel.Compiler;
using System.Data.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ClassLibrary1
{
    //TODO: переименовать таску

    [Designer(typeof(UserTaskDesign))]
    public sealed class UserTask : NativeActivity, IActivityTemplateFactory
    {
        private CompletionCallback<string> onChildComplete;
       
        public InArgument<string> Caption { get; set; }
        public InArgument<string> QueueName { get; set; }
        public InArgument<string> ViewName { get; set; }
        public InArgument<object> ViewInputModel { get; set; }
        public ActivityFunc<Guid, string> Wizard { get; set; }
        public OutArgument<string> Result { get; set; }
        private Variable<Guid> UserTaskId { get; set; } = new Variable<Guid>();
       

        CompletionCallback<string> OnChildComplete
        {
            get
            {
                if (this.onChildComplete == null)
                {
                    this.onChildComplete = new CompletionCallback<string>(WizardFinish);
                }

                return this.onChildComplete;
            }
        }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            metadata.AddImplementationVariable(UserTaskId);
        }

        private void WizardFinish(NativeActivityContext context, ActivityInstance completedinstance, string result)
        {
            RemoveTaskFromDataBase(context);
            Result.Set(context, result);
        }

        private void RemoveTaskFromDataBase(NativeActivityContext context)
        {
            var taskId = UserTaskId.Get(context);
            var db = context.GetExtension<Db>();
            var task = db.UserTasks.Include(t => t.Buttons).First(t => t.Id == taskId);
            db.Buttons.RemoveRange(task.Buttons);
            db.UserTasks.Remove(task);
        }


        protected override void Execute(NativeActivityContext context)
        {
            var task = new UserTaskEntry()
            {
                Id = Guid.NewGuid(),
                Caption = Caption.Get(context),
                QueueName = QueueName.Get(context),
                ViewName = ViewName.Get(context),
                ViewInputModel = JsonConvert.SerializeObject(ViewInputModel.Get(context) ?? new object()
                    , Formatting.None
                    , new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                WWFId = context.WorkflowInstanceId
            };

            var db = context.GetExtension<Db>();
            db.UserTasks.Add(task);

            UserTaskId.Set(context, task.Id);
            
            context.ScheduleFunc(Wizard, task.Id, OnChildComplete);
        }

        public Activity Create(DependencyObject target)
        {
            return new UserTask()
            {
                DisplayName = "Задача пользователю",
                Wizard = new ActivityFunc<Guid, string>
                {
                    Argument = new DelegateInArgument<Guid>("__UserTaskId"),
                    Result = new DelegateOutArgument<string>("Result")
                }
            };
        }
        

        protected override void Cancel(NativeActivityContext context)
        {
            RemoveTaskFromDataBase(context);

            base.Cancel(context);
            
            //TODO: можно добавить параметр "уведомлять при отзыве"
        }
    }
}
