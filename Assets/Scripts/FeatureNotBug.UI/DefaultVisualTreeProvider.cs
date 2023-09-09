using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI;

public sealed class DefaultVisualTreeProvider : IVisualTreeProvider {
    readonly IReadOnlyDictionary<VisualTreeId, VisualTreeAsset> _visualTrees;

    public DefaultVisualTreeProvider(IReadOnlyDictionary<VisualTreeId, VisualTreeAsset> visualTrees) {
        _visualTrees = visualTrees;
    }

    public VisualTreeAsset GetVisualTree(VisualTreeId id) {
        if (!_visualTrees.TryGetValue(id, out var visualTree))
            throw new ArgumentException($"VisualTreeId {id} is not found.");
        return visualTree;
    }

    public AsyncOperationHandle<VisualTreeAsset> GetVisualTreeAsync(VisualTreeId id) {
        if (!_visualTrees.TryGetValue(id, out var visualTree))
            throw new ArgumentException($"VisualTreeId {id} is not found.");
        return Addressables.ResourceManager.CreateCompletedOperation(visualTree, string.Empty);
    }
}
