using Unity.Netcode;
using UnityEngine;
using VContainer;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug {
    [DisallowMultipleComponent]
    [AddComponentMenu("Feature Not Bug/Player Team")]
    [RequireComponent(typeof(Player))]
    public sealed class PlayerTeam : NetworkBehaviour {
        public string? _message;

        IJoinTeamUseCase _joinTeamUseCase = null!;
        Player _player = null!;

        [Inject]
        public void Construct(IJoinTeamUseCase joinTeamUseCase) {
            _joinTeamUseCase = joinTeamUseCase;
        }

        void Awake() {
            _player = GetComponent<Player>();
        }

        public void Join(uint teamId) {
            if (!IsOwner) {
                Debug.LogWarning("Only owner can join team");
                return;
            }

            JoinServerRpc(_player.Id.Value, teamId);
        }

        [ServerRpc]
        void JoinServerRpc(ulong playerId, uint teamId) {
            _joinTeamUseCase.Execute(playerId, teamId);
        }
    }
}
