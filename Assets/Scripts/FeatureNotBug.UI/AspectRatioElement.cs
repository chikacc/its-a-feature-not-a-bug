using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI;

[UxmlElement]
public sealed partial class AspectRatioElement : VisualElement {
    AspectMode _aspectMode;
    float _aspectRatio;

    public AspectRatioElement() : this(AspectMode.FitInParent, 1f) { }

    public AspectRatioElement(AspectMode aspectMode, float aspectRatio) {
        _aspectMode = aspectMode;
        _aspectRatio = aspectRatio;
        style.flexGrow = 1f;
        contentContainer = new VisualElement { style = { position = Position.Absolute } };
        hierarchy.Add(contentContainer);
        UpdateElement();
        generateVisualContent += static e => ((AspectRatioElement)e.visualElement).UpdateElement();
        RegisterCallback<GeometryChangedEvent>(static e => ((AspectRatioElement)e.target).UpdateElement());
    }

    public override VisualElement contentContainer { get; }

    [UxmlAttribute]
    public AspectMode AspectMode { get => _aspectMode; set => Set(ref _aspectMode, value); }

    [Range(0.001f, 1000f)]
    [UxmlAttribute]
    public float AspectRatio { get => _aspectRatio; set => Set(ref _aspectRatio, Mathf.Clamp(value, 0.001f, 1000f)); }

    void Set<T>(ref T field, T value) {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        UpdateElement();
    }

    public void UpdateElement() {
        var (width, height) = (resolvedStyle.width, resolvedStyle.height);
        switch (_aspectMode) {
            case AspectMode.FitInParent when !(height * _aspectRatio < width):
            case AspectMode.EnvelopeParent when height * _aspectRatio < width:
                contentContainer.style.width = width;
                contentContainer.style.height = width / _aspectRatio;
                break;
            case AspectMode.FitInParent:
            case AspectMode.EnvelopeParent:
                contentContainer.style.width = height * _aspectRatio;
                contentContainer.style.height = height;
                break;
        }
    }
}
