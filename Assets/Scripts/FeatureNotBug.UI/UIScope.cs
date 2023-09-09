using Cysharp.Threading.Tasks;
using MessagePipe;
using Unity.Logging;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug.UI {
    [DisallowMultipleComponent]
    [AddComponentMenu("Feature Not Bug/UI Scope")]
    public sealed class UIScope : LifetimeScope {
        public UIDocument? UIDocument;
        public EventSystem? EventSystem;
        public LocalizationSettings? LocalizationSettings;
        public VisualTreeCollectionAsset? VisualTreeCollection;
        public string InitialVisualTreeId = "App";
        public LocalizedReviewPhaseAsset? InitialReviewPhase;

        protected override void Awake() {
            if (UIDocument == null) {
                Log.Error("UIDocument is null");
                return;
            }

            if (UIDocument.rootVisualElement == null) {
                UniTask.Void(static async x => {
                    while (x.UIDocument!.rootVisualElement == null) {
                        x.destroyCancellationToken.ThrowIfCancellationRequested();
                        await UniTask.Yield();
                    }

                    x.Awake();
                }, this);
                return;
            }

            base.Awake();
        }

        void Reset() {
            UIDocument = FindAnyObjectByType<UIDocument>();
            EventSystem = FindAnyObjectByType<EventSystem>();
            LocalizationSettings = LocalizationSettings.Instance;
            VisualTreeCollection = FindAnyObjectByType<VisualTreeCollectionAsset>();
            InitialReviewPhase = new LocalizedReviewPhaseAsset {
                TableReference = "Review Phases",
                TableEntryReference = "Introduce"
            };
        }

        protected override void Configure(IContainerBuilder builder) {
            builder.RegisterInstance(EventSystem);
            builder.RegisterInstance(LocalizationSettings);
            builder.RegisterInstance(VisualTreeCollection!.BuildVisualTreeProvider()).As<IVisualTreeProvider>();

            builder.Register<ITemplateContainerProvider, TemplateContainerProvider>(Lifetime.Scoped);
            builder.Register<ILocalizationService, LocalizationService>(Lifetime.Scoped);
            builder.Register<INavigationService, NavigationService>(Lifetime.Scoped)
                .WithParameter((VisualElement)UIDocument!.rootVisualElement.Q<SafeArea>());

            builder.UseViewModels(viewModels => {
                viewModels.AddScoped<AppViewModel>();
                viewModels.AddScoped<StartViewModel>();
                viewModels.AddScoped<ReviewViewModel>().WithParameter(InitialReviewPhase);

                viewModels.AddScoped<LobbyViewModel>();
                viewModels.AddScoped<LocalizationViewModel>();
                viewModels.AddScoped<AuthenticateViewModel>();
                viewModels.AddScoped<HostViewModel>();
            });

            var options = builder.RegisterMessagePipe(static options =>
                options.HandlingSubscribeDisposedPolicy = HandlingSubscribeDisposedPolicy.Throw);
            builder.UseMessageBrokers(options, static brokers => { });

            builder.RegisterBuildCallback(resolver => {
                var root = UIDocument!.rootVisualElement;
                resolver.InjectVisualElement(root);
                resolver.Resolve<INavigationService>().Push(InitialVisualTreeId);
            });
        }
    }
}
