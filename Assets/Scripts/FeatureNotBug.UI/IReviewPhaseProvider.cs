namespace FeatureNotBug.UI; 

public interface IReviewPhaseProvider {
    ReviewPhaseAsset GetPhase(ReviewPhaseId id);
}