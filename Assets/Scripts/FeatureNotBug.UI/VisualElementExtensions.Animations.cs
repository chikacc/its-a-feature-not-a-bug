using System;
using UnityEngine.UIElements;

// ReSharper disable PartialTypeWithSinglePart

namespace FeatureNotBug.UI; 

public static partial class VisualElementExtensions {
    static readonly Type StylePropertyAnimationsInterfaceType =
        typeof(VisualElement).GetInterface("IStylePropertyAnimations");

    public static void CancelAllAnimations(this VisualElement element) {
        var methodInfo = MethodInfoContainer.Get(StylePropertyAnimationsInterfaceType, "CancelAllAnimations")!;
        methodInfo.Invoke(element, null);
    }
}
