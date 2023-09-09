using Unity.Netcode;

namespace FeatureNotBug;

public sealed class ConditionCardService : NetworkBehaviour {
    readonly CardGroupAsset _cardGroup;

    public ConditionCardService(CardGroupAsset cardGroup) {
        _cardGroup = cardGroup;
    }
}
