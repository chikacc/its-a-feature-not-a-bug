using Unity.Netcode;
using UnityEngine;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug {
    [DisallowMultipleComponent]
    [AddComponentMenu("Feature Not Bug/Team Repository")]
    public sealed class TeamRepository : NetworkBehaviour, ITeamRepository {
        // public readonly NetworkList<Team> Teams = new();

        // public Team this[uint id] => Teams[(int)id];
        // public int Count => Teams.Count;
        // public void Add(Team team) => Teams.Add(team);
    }
}
