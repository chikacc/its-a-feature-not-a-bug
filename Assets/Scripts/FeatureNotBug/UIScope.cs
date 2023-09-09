using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug {
    [DisallowMultipleComponent]
    [AddComponentMenu("Feature Not Bug/UI Scope")]
    public sealed class UIScope : LifetimeScope {
        public UIDocument? UIDocument;
        public EventSystem? EventSystem;
        public LocalizationSettings? LocalizationSettings;
        public LocalizedVisualTreeAsset? InitialVisualTree;
        public LocalizedReviewPhaseAsset? InitialReviewPhase;

        protected override void Awake() {
            if (UIDocument == null) {
                Debug.LogErrorFormat(this, "UIDocument is null");
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
            InitialVisualTree = new LocalizedVisualTreeAsset {
                TableReference = "Visual Trees",
                TableEntryReference = "Start"
            };
            InitialReviewPhase = new LocalizedReviewPhaseAsset {
                TableReference = "Review Phases",
                TableEntryReference = "Introduce"
            };
        }

        protected override void Configure(IContainerBuilder builder) {
            builder.RegisterInstance(EventSystem);
            builder.RegisterInstance(LocalizationSettings);

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

            builder.RegisterBuildCallback(resolver => {
                var root = UIDocument!.rootVisualElement;
                resolver.InjectVisualElement(root);
                resolver.Resolve<INavigationService>().Push(InitialVisualTree!);
            });
        }
    }
}
