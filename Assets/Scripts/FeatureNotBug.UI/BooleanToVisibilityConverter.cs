using UnityEngine;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI; 

public static class BooleanToVisibilityConverter {
    [RuntimeInitializeOnLoadMethod]
    static void OnLoadMethod() {
        var group = new ConverterGroup("BooleanToVisibility");
        group.AddConverter((ref bool v) => v ? (StyleEnum<Visibility>)Visibility.Visible : Visibility.Hidden);
        group.AddConverter((ref StyleEnum<Visibility> v) => v == Visibility.Visible);
        ConverterGroups.RegisterConverterGroup(group);
    }
}
