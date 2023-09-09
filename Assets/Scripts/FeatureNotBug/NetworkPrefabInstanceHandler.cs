using Unity.Netcode;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FeatureNotBug;

public sealed class NetworkPrefabInstanceHandler : INetworkPrefabInstanceHandler {
    readonly IObjectResolver _resolver;

    public NetworkPrefabInstanceHandler(IObjectResolver resolver, NetworkObject prefab) {
        _resolver = resolver;
        Prefab = prefab;
    }

    public NetworkObject Prefab { get; }

    public NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation) {
        return _resolver.Instantiate(Prefab, position, rotation, null);
    }

    public void Destroy(NetworkObject networkObject) {
        Object.Destroy(networkObject.gameObject);
    }
}
