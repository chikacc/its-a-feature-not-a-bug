namespace FeatureNotBug.UI; 

public interface IReviewPhaseCollection {
    IReviewPhaseCollection Add(ReviewPhaseId id, ReviewPhaseAsset asset);
    IReviewPhaseProvider BuildReviewerPhaseProvider();
}