using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.Presentation.View;
using System.Activities.Statements;
using System.ComponentModel;
using Newtonsoft.Json;

namespace ClassLibrary1
{
    [Designer(typeof(CommentButtonDesigner))]
    [DefaultTypeArgument(typeof(EmptyModel))]
  
    public class CommentButton<T> : NativeActivity
        where T:class
    {
        public InArgument<string> Name { get; set; }
        
        public InArgument<int> Priority { get; set; }
        public InArgument<string> Style { get; set; }
        public OutArgument<T> ClientModel { get; set; }
        public OutArgument<T> CommentForStep { get; set; }

        private Variable<Guid> buttonId = new Variable<Guid>();

        protected override bool CanInduceIdle => true;
        

        protected override void Execute(NativeActivityContext context)
        {
            var db = context.GetExtension<Db>();
            var button = new ButtonEntry()
            {
                Id = Guid.NewGuid(),
                Name = Name.Get(context),
                Priority = Priority.Get(context),
                Style = Style.Get(context),
                TypeButton = "comment",
            };

            if (string.IsNullOrEmpty(button.Style)) button.Style = "btn-default";
            
            var userTaskId = (Guid) context.DataContext.GetProperties()["__UserTaskId"].GetValue(context.DataContext);

            var userTask = db.UserTasks.Find(userTaskId);
            userTask.Buttons.Add(button);

            buttonId.Set(context, button.Id);

            context.CreateBookmark(button.Id.ToString(), OnClick);
        }

        void OnClick(NativeActivityContext context, Bookmark bookmark, object value)
        {
            RemoveButton(context);
            if (typeof(T) != typeof(EmptyModel))
            {
                CommentForStep.Set(context, JsonConvert.DeserializeObject<T>((string)value));
            }
        }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            metadata.AddImplementationVariable(buttonId);
        }

        protected override void Cancel(NativeActivityContext context)
        {
            RemoveButton(context);
            base.Cancel(context);
        }

        void RemoveButton(NativeActivityContext context)
        {
            var db = context.GetExtension<Db>();
            var button = db.Buttons.Find(buttonId.Get(context));
            if (button != null)
            {
                db.Buttons.Remove(button);
            }
        }
    }

}
