using UnityEngine.UIElements;

namespace FeatureNotBug.UI; 

public interface IVisualTreeCollection {
    IVisualTreeCollection Add(VisualTreeId id, VisualTreeAsset visualTree);
    IVisualTreeProvider BuildVisualTreeProvider();
}