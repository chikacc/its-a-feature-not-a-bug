using System;
using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug {
    public sealed class CardGroupAsset : ScriptableObject {
        public const string Extension = ".cardgroup.csv";

        public Card[] Cards = Array.Empty<Card>();

        public void Shuffle(int seed) {
            Random.InitState(seed);
            for (var i = 0; i < Cards.Length; i++) {
                var j = Random.Range(i, Cards.Length);
                (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
            }
        }
    }
}
