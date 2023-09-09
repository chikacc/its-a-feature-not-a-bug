using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI;

public sealed class NavigationService : INavigationService {
    const string HiddenUssClassName = "hidden";

    readonly IVisualTreeProvider _visualTreeProvider;
    readonly ITemplateContainerProvider _visualElementProvider;
    readonly VisualElement _root;
    readonly Stack<VisualElement> _navigationStack = new();

    public NavigationService(IVisualTreeProvider visualTreeProvider,
        ITemplateContainerProvider templateContainerProvider, VisualElement root) {
        _visualTreeProvider = visualTreeProvider;
        _visualElementProvider = templateContainerProvider;
        _root = root;
    }

    public int Count => _navigationStack.Count;

    public void Push(VisualTreeId next) {
        UniTask.Void(async () => Push(await _visualTreeProvider.GetVisualTreeAsync(next)));
    }

    public void Push(VisualTreeAsset next) {
        Push(_visualElementProvider.GetTemplateContainer(next));
    }

    public void Push(VisualElement next) {
        if (_navigationStack.TryPeek(out var current)) current.AddToClassList(HiddenUssClassName);
        next.RemoveFromClassList(HiddenUssClassName);
        _root.Add(next);
        _navigationStack.Push(next);
    }

    public void Pop() {
        if (_navigationStack.TryPop(out var previous)) {
            previous.AddToClassList(HiddenUssClassName);
            _root.Remove(previous);
        }

        if (!_navigationStack.TryPeek(out var current)) return;
        current.RemoveFromClassList(HiddenUssClassName);
    }

    public void Clear() {
        while (_navigationStack.TryPop(out var previous)) {
            previous.AddToClassList(HiddenUssClassName);
            _root.Remove(previous);
        }
    }
}
