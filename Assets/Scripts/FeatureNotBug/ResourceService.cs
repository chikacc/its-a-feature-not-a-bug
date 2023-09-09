using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using VContainer.Unity;

namespace FeatureNotBug;

public sealed class ResourceService : IAsyncStartable {
    public async UniTask StartAsync(CancellationToken cancellation) {
        await Addressables.InitializeAsync().WithCancellation(cancellation);
    }
}
