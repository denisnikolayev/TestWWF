using System;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.View;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ClassLibrary1
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class CommentButtonDesigner
    {
        const string TypeArgumentPropertyName = "TypeArgument";

        public CommentButtonDesigner()
        {
            InitializeComponent();
            
        }

        private void CommentButtonDesigner_OnLoaded(object sender, RoutedEventArgs e)
        {
            AttachedProperty<Type> typeArgumentProperty = new AttachedProperty<Type>
            {
                Name = TypeArgumentPropertyName,
                OwnerType = typeof(CommentButton<>),
                Getter = (modelItem) => modelItem.Parent == null ? null : GetTypeArgument(modelItem),
                Setter = (modelItem, value) => UpdateTypeArgument(modelItem, value),
                IsBrowsable = true
            };
            Context.Services.GetService<AttachedPropertiesService>().AddProperty(typeArgumentProperty);
        }

        private static void UpdateTypeArgument(ModelItem modelItem, Type value)
        {
            if (value != null)
            {
                Type oldModelItemType = modelItem.ItemType;
                
                Type newModelItemType = oldModelItemType.GetGenericTypeDefinition().MakeGenericType(value);
               
                ModelItem newModelItem = ModelFactory.CreateItem(modelItem.GetEditingContext(), Activator.CreateInstance(newModelItemType));
                MorphHelper.MorphObject(modelItem, newModelItem);
                MorphHelper.MorphProperties(modelItem, newModelItem);

                if (oldModelItemType.IsSubclassOf(typeof(Activity)) && newModelItemType.IsSubclassOf(typeof(Activity)))
                {
                    if (string.Equals((string)modelItem.Properties["DisplayName"].ComputedValue, GetActivityDefaultName(oldModelItemType), StringComparison.Ordinal))
                    {
                        newModelItem.Properties["DisplayName"].SetValue(GetActivityDefaultName(newModelItemType));
                    }
                }

                DesignerView designerView = modelItem.GetEditingContext().Services.GetService<DesignerView>();
                if (designerView != null)
                {
                    Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Render, (Action)(() =>
                    {
                        if (designerView.RootDesigner != null && ((WorkflowViewElement)designerView.RootDesigner).ModelItem == modelItem)
                        {
                            designerView.MakeRootDesigner(newModelItem);
                        }
                        Selection.SelectOnly(modelItem.GetEditingContext(), newModelItem);
                    }));
                }
            }
        }

        private static Type GetTypeArgument(ModelItem modelItem)
        {
            return modelItem.ItemType.GetGenericArguments()[0];
        }

        private static string GetActivityDefaultName(Type activityType)
        {
            Activity activity = (Activity)Activator.CreateInstance(activityType);
            return activity.DisplayName;
        }
    }
}
