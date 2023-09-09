using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug {
    [CreateAssetMenu(menuName = "Feature Not Bug/UI/Review Phase Collections/Default")]
    public sealed class DefaultReviewPhaseCollectionAsset : ReviewPhaseCollectionAsset {
        [Serializable]
        public struct Item {
            public string Id;
            public ReviewPhaseAsset? Phase;
        }

        public List<Item> Items = new();

        public override IReviewPhaseCollection Add(ReviewPhaseId id, ReviewPhaseAsset asset) {
            Items.Add(new Item { Id = id, Phase = asset });
            return this;
        }

        public override IReviewPhaseProvider BuildReviewerPhaseProvider() {
            var dictionary = new Dictionary<ReviewPhaseId, ReviewPhaseAsset>();
            foreach (var item in Items)
                if (!string.IsNullOrEmpty(item.Id) && item.Phase != null)
                    dictionary[item.Id] = item.Phase;
            return new ReviewPhaseProvider(dictionary);
        }
    }
}
