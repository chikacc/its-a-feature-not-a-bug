using System;
using System.Reflection;
using Unity.Logging;
using UnityEngine.UIElements;
using VContainer;
using Console = System.Console;

namespace FeatureNotBug.UI;

public static class ObjectResolverExtensions {
    public static void InjectVisualElement(this IObjectResolver resolver, VisualElement element) {
        InternalInjectVisualElement(resolver, element);
        for (var i = 0; i < element.childCount; ++i) resolver.InjectVisualElement(element.ElementAt(i));
    }

    static void InternalInjectVisualElement(IObjectResolver resolver, VisualElement element) {
        resolver.Inject(element);
        InjectBindings(resolver, element);
    }

    public static void InjectBindings(this IObjectResolver resolver, VisualElement element) {
        foreach (var bindingInfo in element.GetBindingInfos())
            if (bindingInfo.binding is DataBinding { dataSource: null } binding)
                try {
                    var propertyInfo = PropertyInfoContainer.Get(typeof(DataBinding), "dataSourceType",
                        BindingFlags.Instance | BindingFlags.NonPublic)!;
                    var type = (Type)propertyInfo.GetValue(binding);
                    try {
                        binding.dataSource = resolver.Resolve(type);
                    } catch (Exception e) {
                        Log.Error(e, "Failed to resolve {Type}", type);
                    }
                } catch (Exception e) {
                    Log.Error(e, "Failed to get dataSourceType from {Element}", element);
                }
    }
}
