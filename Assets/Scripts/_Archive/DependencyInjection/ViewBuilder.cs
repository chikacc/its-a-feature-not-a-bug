using UnityEngine.UIElements;
using VContainer;

namespace FeatureNotBug.UI.DependencyInjection;

public readonly struct ViewBuilder {
    readonly IContainerBuilder _containerBuilder;
    readonly VisualElement _root;

    public ViewBuilder(IContainerBuilder containerBuilder, VisualElement root) {
        _containerBuilder = containerBuilder;
        _root = root;
    }

    public RegistrationBuilder Add<T>(string name, Lifetime lifetime) {
        return _containerBuilder.Register<T>(lifetime).WithParameter(_root.Q(name));
    }
}
