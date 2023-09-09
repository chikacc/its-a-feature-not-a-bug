using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

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
        foreach (var bindingInfo in element.GetBindingInfos()) {
            var binding = bindingInfo.binding;
            resolver.Inject(binding);
            if (binding is not DataBinding { dataSource: null } dataBinding) continue;
            try {
                var propertyInfo = PropertyInfoContainer.Get(typeof(DataBinding), "dataSourceType",
                    BindingFlags.Instance | BindingFlags.NonPublic)!;
                var type = (Type)propertyInfo.GetValue(dataBinding);
                try {
                    dataBinding.dataSource = resolver.Resolve(type);
                } catch (Exception e) {
                    Debug.LogErrorFormat("Failed to resolve {0}: {1}", type, e);
                }
            } catch (Exception e) {
                Debug.LogErrorFormat("Failed to get dataSourceType from {0}: {1}", element, e);
            }
        }
    }
}
