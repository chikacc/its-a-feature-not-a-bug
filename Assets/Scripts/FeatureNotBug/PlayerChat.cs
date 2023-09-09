using Unity.Netcode;
using UnityEngine;
using VContainer;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug {
    [DisallowMultipleComponent]
    [AddComponentMenu("Feature Not Bug/Player Chat")]
    [RequireComponent(typeof(Player))]
    public sealed class PlayerChat : NetworkBehaviour {
        public string? _message;

        ISubmitChatUseCase _submitChatUseCase = null!;
        Player _player = null!;

        [Inject]
        public void Construct(ISubmitChatUseCase submitChatUseCase) {
            _submitChatUseCase = submitChatUseCase;
        }

        void Awake() {
            _player = GetComponent<Player>();
        }

        public void Submit(string message) {
            if (!IsOwner) {
                Debug.LogWarning("Only owner can submit chat.");
                return;
            }

            SubmitServerRpc(_player.Id.Value, message);
        }

        [ServerRpc]
        void SubmitServerRpc(ulong playerId, string message) {
            _submitChatUseCase.Execute(playerId, message);
        }
    }
}
