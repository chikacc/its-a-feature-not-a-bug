using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI; 

public interface IVisualTreeProvider {
    VisualTreeAsset GetVisualTree(VisualTreeId id);
    AsyncOperationHandle<VisualTreeAsset> GetVisualTreeAsync(VisualTreeId id);
}
