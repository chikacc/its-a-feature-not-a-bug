using Unity.Logging;
using UnityEngine;
using VContainer;
using VContainer.Unity;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug {
    [DisallowMultipleComponent]
    [AddComponentMenu("")]
    public sealed class ApplicationScope : LifetimeScope {
        protected override void Configure(IContainerBuilder builder) {
            builder.RegisterEntryPointExceptionHandler(e => Log.Error("Unhandled exception {e}", e));
        }
    }
}
