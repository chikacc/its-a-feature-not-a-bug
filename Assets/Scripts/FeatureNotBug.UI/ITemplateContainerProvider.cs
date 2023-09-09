using UnityEngine.UIElements;

namespace FeatureNotBug.UI; 

public interface ITemplateContainerProvider {
    TemplateContainer GetTemplateContainer(VisualTreeAsset visualTree);
}