using System;
using MessagePipe;
using VContainer;

// ReSharper disable PartialTypeWithSinglePart

namespace FeatureNotBug;

public static partial class ContainerBuilderExtensions {
    public static void UseMessageBrokers(this IContainerBuilder builder, MessagePipeOptions options,
        Action<MessageBrokersBuilder> configuration) {
        configuration(new MessageBrokersBuilder(builder, options));
    }

    public readonly struct MessageBrokersBuilder {
        readonly IContainerBuilder _containerBuilder;
        readonly MessagePipeOptions _options;

        public MessageBrokersBuilder(IContainerBuilder containerBuilder, MessagePipeOptions options) {
            _containerBuilder = containerBuilder;
            _options = options;
        }

        public void Add<TMessage>() {
            _containerBuilder.RegisterMessageBroker<TMessage>(_options);
        }

        public void Add<TKey, TMessage>() {
            _containerBuilder.RegisterMessageBroker<TKey, TMessage>(_options);
        }

        public void AddRequestHandler<TRequest, TResponse, THandler>() where THandler : IRequestHandler {
            _containerBuilder.RegisterRequestHandler<TRequest, TResponse, THandler>(_options);
        }

        public void AddAsyncRequestHandler<TRequest, TResponse, THandler>() where THandler : IAsyncRequestHandler {
            _containerBuilder.RegisterAsyncRequestHandler<TRequest, TResponse, THandler>(_options);
        }

        public void AddFilter<T>() where T : class, IMessageHandlerFilter {
            _containerBuilder.RegisterMessageHandlerFilter<T>();
        }

        public void AddAsyncFilter<T>() where T : class, IAsyncMessageHandlerFilter {
            _containerBuilder.RegisterAsyncMessageHandlerFilter<T>();
        }

        public void AddRequestHandlerFilter<T>() where T : class, IRequestHandlerFilter {
            _containerBuilder.RegisterRequestHandlerFilter<T>();
        }

        public void AddAsyncRequestHandlerFilter<T>() where T : class, IAsyncRequestHandlerFilter {
            _containerBuilder.RegisterAsyncRequestHandlerFilter<T>();
        }
    }
}
