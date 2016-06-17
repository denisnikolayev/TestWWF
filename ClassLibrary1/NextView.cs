using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ClassLibrary1
{

    public sealed class NextView : CodeActivity
    {
        public InArgument<string> ViewName { get; set; }
        public InArgument<object> ViewInputModel { get; set; }
        
        protected override void Execute(CodeActivityContext context)
        {
            var userTaskId = (Guid)context.DataContext.GetProperties()["__UserTaskId"].GetValue(context.DataContext);
            var db = context.GetExtension<Db>();
            var userTask = db.UserTasks.Find(userTaskId);
            userTask.ViewName = ViewName.Get(context);
            userTask.ViewInputModel = JsonConvert.SerializeObject(ViewInputModel.Get(context) ?? new object()
                , Formatting.None
                , new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver(), DateFormatString = "dd.MM.yyyy"}
                );
        }
    }
}
