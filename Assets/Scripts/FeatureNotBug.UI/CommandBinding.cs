using System;
using System.Reflection;
using System.Windows.Input;
using Unity.Logging;
using Unity.Properties;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI;

[UxmlObject]
public sealed partial class CommandBinding : DataBinding {
    public CommandBinding() {
        updateTrigger = BindingUpdateTrigger.OnSourceChanged;
    }

    public ICommand Command { get; set; } = new RelayCommand(static _ => throw new InvalidOperationException());

    [UxmlAttribute]
    public string Parameter { get; set; } = string.Empty;

    void Execute() {
        if (!Command.CanExecute(Parameter)) {
            Log.Error("Command {Command} cannot execute with parameter {Parameter}", Command, Parameter);
            return;
        }

        Command.Execute(Parameter);
    }

    string GetProperty() {
        var propertyInfo =
            PropertyInfoContainer.Get(typeof(Binding), "property", BindingFlags.Instance | BindingFlags.NonPublic)!;
        return (string)propertyInfo.GetValue(this);
    }

    EventInfo GetEventInfo(object obj) {
        var property = GetProperty();
        var type = obj.GetType();
        return EventInfoContainer.Get(type, property)!;
    }

    protected override void OnActivated(in BindingActivationContext context) {
        var element = context.targetElement;
        var eventInfo = GetEventInfo(element);
        eventInfo.AddEventHandler(element, Execute);
    }

    protected override void OnDeactivated(in BindingActivationContext context) {
        var element = context.targetElement;
        var eventInfo = GetEventInfo(element);
        eventInfo.RemoveEventHandler(element, Execute);
    }

    protected override void OnDataSourceChanged(in DataSourceContextChanged context) {
        var source = context.newContext.dataSource;
        var sourcePath = context.newContext.dataSourcePath;
        Command = PropertyContainer.GetValue<object, ICommand>(source, sourcePath);
    }
}
