using UnityEngine.AddressableAssets;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI;

public sealed class LocalizedAssetTableVisualTreeProvider : IVisualTreeProvider {
    readonly LocalizedAssetTable _table;

    public LocalizedAssetTableVisualTreeProvider(LocalizedAssetTable table) {
        _table = table;
    }

    public VisualTreeAsset GetVisualTree(VisualTreeId id) {
        var table = _table.GetTable();
        return table.GetAssetAsync<VisualTreeAsset>(id).WaitForCompletion();
    }

    public AsyncOperationHandle<VisualTreeAsset> GetVisualTreeAsync(VisualTreeId id) {
        return Addressables.ResourceManager.CreateChainOperation(_table.GetTableAsync(),
            handle => handle.Result.GetAssetAsync<VisualTreeAsset>(id));
    }
}
