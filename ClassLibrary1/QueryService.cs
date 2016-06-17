using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Reflection;

namespace ClassLibrary1
{

    public sealed class QueryService<TOut> : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> ServiceName { get; set; }
        public InArgument<object[]> InParams { get; set; }
        public OutArgument<TOut> Result { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            //TODO: реализовать пример объернутого вызова сервиса

            var cls = Assembly.Load("WebApplication1").GetType("WebApplication1.RequestCard.RequestCardServices");
            var method = cls.GetMethod(ServiceName.Get(context));
            Result.Set(context, (TOut) method.Invoke(null, InParams.Get(context)));
        }
    }
}
