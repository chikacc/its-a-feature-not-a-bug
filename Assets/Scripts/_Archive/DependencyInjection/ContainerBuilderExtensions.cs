using System;
using UnityEngine.UIElements;
using VContainer;

// ReSharper disable PartialTypeWithSinglePart

namespace FeatureNotBug.UI.DependencyInjection;

public static partial class ContainerBuilderExtensions {
    public static void UseViews(this IContainerBuilder builder, VisualElement root, Action<ViewBuilder> configuration) {
        configuration(new ViewBuilder(builder, root));
    }

    public static void UsePresenters(this IContainerBuilder builder, Action<PresenterBuilder> configuration) {
        configuration(new PresenterBuilder(builder));
    }
}
