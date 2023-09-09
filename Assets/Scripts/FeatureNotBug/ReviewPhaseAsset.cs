using UnityEngine;
using UnityEngine.Localization;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug {
    [CreateAssetMenu(menuName = "Feature Not Bug/UI/Review Phase")]
    public sealed class ReviewPhaseAsset : ScriptableObject {
        public Color AccentColor = Color.clear;
        public LocalizedString Title = new();
        public LocalizedString Description = new();
        public float TimerDuration;
        public LocalizedString Next = new();
        public LocalizedReviewPhaseAsset NextReference = new();
    }
}
