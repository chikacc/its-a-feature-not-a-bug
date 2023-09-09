using Unity.Netcode;
using UnityEngine;
using VContainer;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug {
    [DisallowMultipleComponent]
    [AddComponentMenu("Feature Not Bug/Player")]
    public sealed class Player : NetworkBehaviour {
        public NetworkVariable<ulong> Id = new();

        NetworkManager _networkManager = null!;

        [Inject]
        public void Construct(NetworkManager networkManager) {
            _networkManager = networkManager;
        }

        public override void OnNetworkSpawn() {
            if (IsOwner) InitializeServerRpc(_networkManager.LocalClientId);
        }

        [ServerRpc]
        void InitializeServerRpc(ulong id) {
            Id.Value = id;
        }
    }
}
