using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI; 

public sealed class NavigationService : INavigationService {
    const string HiddenUssClassName = "hidden";

    readonly ITemplateContainerProvider _visualElementProvider;
    readonly VisualElement _root;
    readonly Stack<VisualElement> _navigationStack = new();
    readonly LocalizedVisualTreeAsset _localizedVisualTree = new() { TableReference = "Visual Trees" };

    public NavigationService(ITemplateContainerProvider templateContainerProvider, VisualElement root) {
        _visualElementProvider = templateContainerProvider;
        _root = root;
    }

    public int Count => _navigationStack.Count;

    public void Push(string next) {
        _localizedVisualTree.TableEntryReference = next;
        Push(_localizedVisualTree);
    }

    public void Push(LocalizedVisualTreeAsset next) {
        UniTask.Void(async () => Push(await next.LoadAssetAsync()));
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
