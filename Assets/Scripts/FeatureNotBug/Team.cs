using Unity.Netcode;

namespace FeatureNotBug;

public sealed class Team : NetworkBehaviour {
    public NetworkVariable<uint> Id = new();
    public NetworkList<ulong> PlayerIds = new();
}
