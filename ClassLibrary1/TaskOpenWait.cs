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
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ClassLibrary1
{
    [Designer(typeof(TaskOpenWaitDesign))]
    public sealed class TaskOpenWait : NativeActivity
    {
        public InArgument<string> Caption { get; set; }
        public InArgument<object> ViewInputModel { get; set; }

        public Activity Create(DependencyObject target)
        {
            return new TaskOpenWait()
            {
                DisplayName = "Задача ожидания ответа"
            };
        }

        protected override bool CanInduceIdle => true;

        protected override void Execute(NativeActivityContext context)
        {
            var cls = Assembly.Load("WebApplication1").GetType("WebApplication1.Services.RequestAnswerServices");
            var method = cls.GetMethod("OpenWaitTest");
            string bookmark = Guid.NewGuid().ToString();
            method.Invoke(null, new object[] {context.WorkflowInstanceId, bookmark });
            context.CreateBookmark(bookmark);

            var approvalProcess = new ApprovalProcessEntry()
            {
                Id = Guid.NewGuid(),
                Approve = false,
                QueueName = "Ожидающие",
                Bookmark = bookmark,
                WwfId = context.WorkflowInstanceId,
                Info = Caption.Get(context)
            };

            var db = context.GetExtension<Db>();
            db.ApprovalProcess.Add(approvalProcess);
        }
    }
}
