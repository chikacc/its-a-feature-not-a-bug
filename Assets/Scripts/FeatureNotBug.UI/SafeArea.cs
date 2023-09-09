using UnityEngine;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI;

[UxmlElement]
public sealed partial class SafeArea : VisualElement {
    public SafeArea() {
        style.position = Position.Absolute;
        style.top = 0f;
        style.left = 0f;
        style.width = Length.Percent(100f);
        style.height = Length.Percent(100f);
        contentContainer = new VisualElement { style = { flexGrow = 1f } };
        hierarchy.Add(contentContainer);
        RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
    }

    public override VisualElement contentContainer { get; }

    void OnGeometryChanged(GeometryChangedEvent evt) {
        var safeArea = Screen.safeArea;
        contentContainer.style.paddingTop = safeArea.yMin;
        contentContainer.style.paddingRight = Screen.width - safeArea.xMax;
        contentContainer.style.paddingBottom = Screen.height - safeArea.yMax;
        contentContainer.style.paddingLeft = safeArea.xMin;
    }
}
