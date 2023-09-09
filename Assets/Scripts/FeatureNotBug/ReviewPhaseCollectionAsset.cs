using UnityEngine;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug {
    public abstract class ReviewPhaseCollectionAsset : ScriptableObject, IReviewPhaseCollection {
        public abstract IReviewPhaseCollection Add(ReviewPhaseId id, ReviewPhaseAsset asset);
        public abstract IReviewPhaseProvider BuildReviewerPhaseProvider();
    }
}
