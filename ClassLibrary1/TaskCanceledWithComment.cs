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

    [Designer(typeof(TaskCanceledWithCommentDesign))]
    public sealed class TaskCanceledWithComment : NativeActivity, IActivityTemplateFactory
    {
      
        public InArgument<string> Caption { get; set; }
        public InArgument<string> QueueName { get; set; }
        public InArgument<string> InputComment { get; set; }
        private Variable<Guid> TaskCanceledId { get; set; } = new Variable<Guid>();
       

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            metadata.AddImplementationVariable(TaskCanceledId);
        }

        protected override void Execute(NativeActivityContext context)
        {
            var taskCanceledWithComment = new TaskCanceledWithCommentEntry()
            {
                Id = Guid.NewGuid(),
                Caption = Caption.Get(context),
                QueueName = QueueName.Get(context),
                TextComment = InputComment.Get(context),
            };

            var db = context.GetExtension<Db>();
            db.TaskCanceledWithComment.Add(taskCanceledWithComment);

            TaskCanceledId.Set(context, taskCanceledWithComment.Id);
        }

        public Activity Create(DependencyObject target)
        {
            return new TaskCanceledWithComment()
            {
                DisplayName = ""
            };
        }
    }
}
