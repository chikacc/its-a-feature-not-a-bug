using UnityEngine;
using UnityEngine.UIElements;

namespace FeatureNotBug; 

public static class BooleanToDisplayStyleConverter {
    [RuntimeInitializeOnLoadMethod]
    static void OnLoadMethod() {
        var group = new ConverterGroup("BooleanToDisplayStyle");
        group.AddConverter((ref bool v) => v ? (StyleEnum<DisplayStyle>)DisplayStyle.Flex : DisplayStyle.None);
        group.AddConverter((ref StyleEnum<DisplayStyle> v) => v == DisplayStyle.Flex);
        ConverterGroups.RegisterConverterGroup(group);
    }
}
