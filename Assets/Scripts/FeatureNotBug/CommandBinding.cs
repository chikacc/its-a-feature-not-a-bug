using System;
using System.Reflection;
using System.Windows.Input;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace FeatureNotBug;

[UxmlObject]
public sealed partial class CommandBinding : DataBinding {
    public CommandBinding() {
        updateTrigger = BindingUpdateTrigger.OnSourceChanged;
    }

    public ICommand? Command { get; set; }

    [UxmlAttribute]
    public string Parameter { get; set; } = string.Empty;

    void Execute() {
        if (Command == null) {
            Debug.LogErrorFormat("Command is null");
            return;
        }

        if (!Command.CanExecute(Parameter)) {
            Debug.LogErrorFormat("Command {0} cannot execute with parameter {1}", Command, Parameter);
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
        eventInfo.AddEventHandler(element, new Action(Execute));
    }

    protected override void OnDeactivated(in BindingActivationContext context) {
        var element = context.targetElement;
        var eventInfo = GetEventInfo(element);
        eventInfo.RemoveEventHandler(element, new Action(Execute));
    }

    protected override void OnDataSourceChanged(in DataSourceContextChanged context) {
        if (context.newContext.dataSource is not { } source) {
            Command = null;
            return;
        }

        var sourcePath = context.newContext.dataSourcePath;
        Command = PropertyContainer.GetValue<object, ICommand>(source, sourcePath);
    }
}
