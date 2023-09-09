using System.Collections;
using System.Reflection;
using Unity.Properties;
using UnityEngine.UIElements;
using VContainer;

namespace FeatureNotBug.UI;

[UxmlObject]
public sealed partial class StringItemsSourceBinding : DataBinding {
    IObjectResolver? _resolver;

    public StringItemsSourceBinding() {
        updateTrigger = BindingUpdateTrigger.OnSourceChanged;
    }

    [UxmlAttribute]
    public string StringElement { get; set; } = string.Empty;

    [UxmlAttribute]
    public string StringProperty { get; set; } = string.Empty;

    [Inject]
    public void Construct(IObjectResolver resolver) {
        _resolver = resolver;
    }

    protected override void OnDataSourceChanged(in DataSourceContextChanged context) {
        var property = GetProperty();
        var element = context.targetElement;
        var itemsSource = GetItemsSource(context);
        PropertyContainer.SetValue(element, property, itemsSource);
        if (element is not ListView listView) return;
        listView.bindItem = (e, i) => {
            _resolver?.InjectVisualElement(e);
            PropertyContainer.SetValue(e.Q(StringElement), StringProperty, listView.itemsSource[i]);
        };
    }

    string GetProperty() {
        var propertyInfo =
            PropertyInfoContainer.Get(typeof(Binding), "property", BindingFlags.Instance | BindingFlags.NonPublic)!;
        return (string)propertyInfo.GetValue(this);
    }

    static IList? GetItemsSource(in DataSourceContextChanged context) {
        if (context.newContext.dataSource is not { } source) return null;
        var sourcePath = context.newContext.dataSourcePath;
        return PropertyContainer.GetValue<object, IList>(source, sourcePath);
    }
}
