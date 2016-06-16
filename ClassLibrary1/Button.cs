using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    [Designer(typeof(ResultButtonDesigner))]
    public class Button : ResultButton<EmptyModel>
    {

    }
}
