using Unity.Netcode;
using UnityEngine;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug {
    [DisallowMultipleComponent]
    [AddComponentMenu("Feature Not Bug/Card Repository")]
    public sealed class CardRepository : NetworkBehaviour, ICardRepository {
        public readonly NetworkList<Card> Cards = new();

        public Card this[uint id] => Cards[(int)id];
        public int Count => Cards.Count;
        public void Add(Card card) => Cards.Add(card);
    }
}
