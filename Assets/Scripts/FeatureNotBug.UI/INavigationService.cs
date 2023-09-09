using UnityEngine.UIElements;

namespace FeatureNotBug.UI; 

public interface INavigationService {
    int Count { get; }
    void Push(VisualTreeId next);
    void Push(VisualTreeAsset next);
    void Push(VisualElement next);
    void Pop();
    void Clear();
}