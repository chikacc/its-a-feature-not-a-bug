using Cysharp.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.AddressableAssets;
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

        void Reset() {
            NetworkManager = FindAnyObjectByType<NetworkManager>();
            UnityTransport = FindAnyObjectByType<UnityTransport>();
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

            builder.UseServices(services => {
                services.AddScoped<IAuthenticateService, AuthenticateService>();
                services.AddScoped<IHostService, HostService>();
                services.AddScoped<IClientService, ClientService>();
                services.AddScoped<IQrCodeService, QrCodeService>();
                services.AddScoped<IWebCamService, WebCamService>();
            });
        }
    }
}
