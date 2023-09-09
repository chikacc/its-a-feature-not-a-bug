using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug.UI {
    [CreateAssetMenu(menuName = "Feature Not Bug/UI/Visual Tree Collections/Localized Asset")]
    public sealed class LocalizedAssetVisualTreeCollectionAsset : VisualTreeCollectionAsset {
        public LocalizedAssetTable Table = new();

        public IVisualTreeCollection Add(VisualTreeAsset visualTree) {
            return Add(visualTree.name, visualTree);
        }

        public override IVisualTreeCollection Add(VisualTreeId id, VisualTreeAsset visualTree) {
#if UNITY_EDITOR
            var table = Table.GetTable();
            var path = AssetDatabase.GetAssetPath(visualTree);
            var guid = AssetDatabase.AssetPathToGUID(path);
            table.AddEntry(id, guid);
#else
            Debug.LogError("Cannot add visual tree to localized asset table outside of editor.");
#endif
            return this;
        }

        public override IVisualTreeProvider BuildVisualTreeProvider() {
            return new LocalizedAssetTableVisualTreeProvider(Table);
        }
    }
}
