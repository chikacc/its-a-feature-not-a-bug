using System;
using VContainer;

// ReSharper disable PartialTypeWithSinglePart

namespace FeatureNotBug;

public static partial class ContainerBuilderExtensions {
    public static void UseViewModels(this IContainerBuilder builder, Action<ViewModelsBuilder> configuration) {
        configuration(new ViewModelsBuilder(builder));
    }

    public readonly struct ViewModelsBuilder {
        readonly IContainerBuilder _containerBuilder;

        public ViewModelsBuilder(IContainerBuilder containerBuilder) {
            _containerBuilder = containerBuilder;
        }

        public RegistrationBuilder AddTransient<T>() {
            return _containerBuilder.Register<T>(Lifetime.Transient);
        }

        public RegistrationBuilder AddScoped<T>() {
            return _containerBuilder.Register<T>(Lifetime.Scoped);
        }

        public RegistrationBuilder AddSingleton<T>() {
            return _containerBuilder.Register<T>(Lifetime.Singleton);
        }
    }
}
