using System.Collections.Generic;

namespace FeatureNotBug.UI; 

public sealed class ReviewPhaseProvider : IReviewPhaseProvider {
    readonly IReadOnlyDictionary<ReviewPhaseId, ReviewPhaseAsset> _dictionary;

    public ReviewPhaseProvider(IReadOnlyDictionary<ReviewPhaseId, ReviewPhaseAsset> dictionary) {
        _dictionary = dictionary;
    }

    public ReviewPhaseAsset GetPhase(ReviewPhaseId id) {
        return _dictionary[id];
    }
}