using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.Extensions;
using VContainer;
using VContainer.Unity;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug {
    [DisallowMultipleComponent]
    [AddComponentMenu("Feature Not Bug/Dependency Injection/Scene Scope")]
    public sealed class SceneScope : LifetimeScope {
        static bool _addressablesInitialized;

        public NetworkManager? NetworkManager;
        public UnityTransport? UnityTransport;
        public NetworkObject? PlayerPrefab;
        public ChatRepository? ChatRepository;
        public CardRepository? CardRepository;

        void Reset() {
            NetworkManager = FindAnyObjectByType<NetworkManager>();
            UnityTransport = FindAnyObjectByType<UnityTransport>();
            ChatRepository = FindAnyObjectByType<ChatRepository>();
            CardRepository = FindAnyObjectByType<CardRepository>();
        }

        protected override void Awake() {
            if (!_addressablesInitialized) {
                UniTask.Void(static async x => {
                    await Addressables.InitializeAsync();
                    _addressablesInitialized = true;
                    x.Awake();
                }, this);
                return;
            }

            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder) {
            builder.RegisterInstance(Addressables.ResourceManager);
            builder.RegisterInstance(NetworkManager);
            builder.RegisterInstance(UnityTransport);
            builder.RegisterInstance<IChatRepository>(ChatRepository!);
            builder.RegisterInstance<ICardRepository>(CardRepository!);

            builder.UseServices(services => {
                services.AddScoped<IAuthenticateService, AuthenticateService>();
                services.AddScoped<IHostService, HostService>();
                services.AddScoped<IClientService, ClientService>();
                services.AddScoped<IQrCodeService, QrCodeService>();
                services.AddScoped<IWebCamService, WebCamService>();
            });

            if (PlayerPrefab != null) {
                builder.Register<INetworkPrefabInstanceHandler, NetworkPrefabInstanceHandler>(Lifetime.Scoped)
                    .WithParameter(PlayerPrefab);
                builder.RegisterBuildCallback(resolver => {
                    var networkManager = resolver.Resolve<NetworkManager>();
                    var networkPrefabInstanceHandlers = resolver.Resolve<IEnumerable<INetworkPrefabInstanceHandler>>();
                    foreach (var networkPrefabInstanceHandler in networkPrefabInstanceHandlers)
                        if (networkPrefabInstanceHandler is NetworkPrefabInstanceHandler handler)
                            networkManager.PrefabHandler.AddHandler(handler.Prefab, handler);
                });
            }

            builder.Register<ISubmitChatUseCase, SubmitChatUseCase>(Lifetime.Scoped);
        }
    }
}
