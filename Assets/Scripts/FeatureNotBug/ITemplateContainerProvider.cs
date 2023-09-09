using UnityEngine.UIElements;

namespace FeatureNotBug; 

public interface ITemplateContainerProvider {
    TemplateContainer GetTemplateContainer(VisualTreeAsset visualTree);
}