﻿// <auto-generated/>
using System;
using VContainer;

namespace FeatureNotBug;

public static partial class ContainerBuilderExtensions {
    public static void UseServices(this IContainerBuilder builder, Action<ServicesBuilder> configuration) {
        configuration(new ServicesBuilder(builder));
    }

    public readonly struct ServicesBuilder {
        readonly IContainerBuilder _containerBuilder;

        public ServicesBuilder(IContainerBuilder containerBuilder) {
            _containerBuilder = containerBuilder;
        }

        public RegistrationBuilder AddTransient<TImplement>() {
            return _containerBuilder.Register<TImplement>(Lifetime.Transient);
        }

        public RegistrationBuilder AddSingleton<TImplement>() {
            return _containerBuilder.Register<TImplement>(Lifetime.Singleton);
        }

        public RegistrationBuilder AddScoped<TImplement>() {
            return _containerBuilder.Register<TImplement>(Lifetime.Scoped);
        }

        public RegistrationBuilder AddTransient<Interface1, TImplement>() where TImplement : Interface1 {
            return _containerBuilder.Register<Interface1, TImplement>(Lifetime.Transient);
        }

        public RegistrationBuilder AddSingleton<Interface1, TImplement>() where TImplement : Interface1 {
            return _containerBuilder.Register<Interface1, TImplement>(Lifetime.Singleton);
        }

        public RegistrationBuilder AddScoped<Interface1, TImplement>() where TImplement : Interface1 {
            return _containerBuilder.Register<Interface1, TImplement>(Lifetime.Scoped);
        }

        public RegistrationBuilder AddTransient<Interface1, Interface2, TImplement>() where TImplement : Interface1, Interface2 {
            return _containerBuilder.Register<Interface1, Interface2, TImplement>(Lifetime.Transient);
        }

        public RegistrationBuilder AddSingleton<Interface1, Interface2, TImplement>() where TImplement : Interface1, Interface2 {
            return _containerBuilder.Register<Interface1, Interface2, TImplement>(Lifetime.Singleton);
        }

        public RegistrationBuilder AddScoped<Interface1, Interface2, TImplement>() where TImplement : Interface1, Interface2 {
            return _containerBuilder.Register<Interface1, Interface2, TImplement>(Lifetime.Scoped);
        }

        public RegistrationBuilder AddTransient<Interface1, Interface2, Interface3, TImplement>() where TImplement : Interface1, Interface2, Interface3 {
            return _containerBuilder.Register<Interface1, Interface2, Interface3, TImplement>(Lifetime.Transient);
        }

        public RegistrationBuilder AddSingleton<Interface1, Interface2, Interface3, TImplement>() where TImplement : Interface1, Interface2, Interface3 {
            return _containerBuilder.Register<Interface1, Interface2, Interface3, TImplement>(Lifetime.Singleton);
        }

        public RegistrationBuilder AddScoped<Interface1, Interface2, Interface3, TImplement>() where TImplement : Interface1, Interface2, Interface3 {
            return _containerBuilder.Register<Interface1, Interface2, Interface3, TImplement>(Lifetime.Scoped);
        }
    }
}
