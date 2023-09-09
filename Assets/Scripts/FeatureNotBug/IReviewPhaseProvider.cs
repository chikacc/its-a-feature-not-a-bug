namespace FeatureNotBug; 

public interface IReviewPhaseProvider {
    ReviewPhaseAsset GetPhase(ReviewPhaseId id);
}