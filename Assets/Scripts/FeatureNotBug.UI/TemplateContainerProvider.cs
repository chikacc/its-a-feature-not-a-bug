using UnityEngine.UIElements;
using VContainer;

namespace FeatureNotBug.UI; 

public sealed class TemplateContainerProvider : ITemplateContainerProvider {
    readonly IObjectResolver _resolver;

    public TemplateContainerProvider(IObjectResolver resolver) {
        _resolver = resolver;
    }

    public TemplateContainer GetTemplateContainer(VisualTreeAsset visualTree) {
        var element = visualTree.Instantiate();
        element.style.flexGrow = 1;
        _resolver.InjectVisualElement(element);
        return element;
    }
}