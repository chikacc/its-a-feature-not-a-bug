using UnityEngine;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug.UI {
    public abstract class ReviewPhaseCollectionAsset : ScriptableObject, IReviewPhaseCollection {
        public abstract IReviewPhaseCollection Add(ReviewPhaseId id, ReviewPhaseAsset asset);
        public abstract IReviewPhaseProvider BuildReviewerPhaseProvider();
    }
}
