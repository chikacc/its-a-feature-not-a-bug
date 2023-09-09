using Unity.Netcode;
using UnityEngine;
using Unity.Logging;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug {
    [DisallowMultipleComponent]
    [AddComponentMenu("Feature Not Bug/Player")]
    public sealed class Player : NetworkBehaviour {
        public override void OnNetworkSpawn() {
            if (!IsServer && IsOwner) TestServerRpc(0, NetworkObjectId);
        }

        [ClientRpc]
        void TestClientRpc(int value, ulong sourceNetworkObjectId) {
            Log.Info("Client Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}", value, sourceNetworkObjectId);
            if (IsOwner) TestServerRpc(value + 1, sourceNetworkObjectId);
        }

        [ServerRpc]
        void TestServerRpc(int value, ulong sourceNetworkObjectId) {
            Log.Info("Server Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}", value, sourceNetworkObjectId);
            TestClientRpc(value, sourceNetworkObjectId);
        }
    }
}
