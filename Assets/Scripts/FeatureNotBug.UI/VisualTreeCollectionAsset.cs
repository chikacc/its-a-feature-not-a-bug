using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug.UI {
    public abstract class VisualTreeCollectionAsset : ScriptableObject, IVisualTreeCollection {
        public abstract IVisualTreeCollection Add(VisualTreeId id, VisualTreeAsset visualTree);
        public abstract IVisualTreeProvider BuildVisualTreeProvider();
    }
}
