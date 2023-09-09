using VContainer;

namespace FeatureNotBug.UI.DependencyInjection;

public readonly struct PresenterBuilder {
    readonly IContainerBuilder _containerBuilder;

    public PresenterBuilder(IContainerBuilder containerBuilder) {
        _containerBuilder = containerBuilder;
    }

    public RegistrationBuilder Add<T>(Lifetime lifetime) {
        return _containerBuilder.Register<T>(lifetime);
    }

    public RegistrationBuilder AddNonLazy<T>(Lifetime lifetime) {
        _containerBuilder.RegisterBuildCallback(resolver => resolver.Resolve<T>());
        return _containerBuilder.Register<T>(lifetime);
    }
}
