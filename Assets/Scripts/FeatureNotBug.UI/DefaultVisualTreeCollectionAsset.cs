using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug.UI {
[CreateAssetMenu(fileName = "VisualTreeCollectionAsset",
    menuName = "Feature Not Bug/UI/Visual Tree Collections/Default")]
    public sealed class DefaultVisualTreeCollectionAsset : VisualTreeCollectionAsset {
        [Serializable]
        public struct Item {
            public string Id;
            public VisualTreeAsset? VisualTree;
        }

        public List<Item> Items = new();

        public IVisualTreeCollection Add(VisualTreeAsset visualTree) {
            Items.Add(new Item { Id = visualTree.name, VisualTree = visualTree });
            return this;
        }

        public override IVisualTreeCollection Add(VisualTreeId id, VisualTreeAsset visualTree) {
            Items.Add(new Item { Id = id, VisualTree = visualTree });
            return this;
        }

        public override IVisualTreeProvider BuildVisualTreeProvider() {
            var dictionary = new Dictionary<VisualTreeId, VisualTreeAsset>();
            foreach (var item in Items)
                if (item.VisualTree != null)
                    dictionary[item.Id] = item.VisualTree;
            return new DefaultVisualTreeProvider(dictionary);
        }
    }
}
