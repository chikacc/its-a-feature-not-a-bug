using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Localization;
using UnityEditor.Localization.UI;
using UnityEditor.UIElements;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI.Editor.FeatureNotBug.UI.Editor;

[CustomEditor(typeof(LocalizedAssetVisualTreeCollectionAsset))]
public sealed class LocalizedAssetTableVisualTreeCollectionAssetEditor : UnityEditor.Editor {
    public struct Item {
        public string Id;
        public VisualTreeAsset? VisualTree;
    }

    public override VisualElement CreateInspectorGUI() {
        var root = new VisualElement();
        InspectorElement.FillDefaultInspector(root, serializedObject, this);
        var table = ((LocalizedAssetVisualTreeCollectionAsset)target).Table;
        var listView = new MultiColumnTreeView {
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
        };
        listView.SetRootItems(GetRootItems());
        table.TableChanged += _ => listView.SetRootItems(GetRootItems());
        listView.columns.Add(new Column { name = "id", title = "Id", stretchable = true });
        listView.columns.Add(new Column { name = "visualTree", title = "Visual Tree", stretchable = true });
        listView.columns["id"].makeCell = () => new TextElement();
        listView.columns["visualTree"].makeCell = () => {
            var objectField = new ObjectField { objectType = typeof(VisualTreeAsset) };
            objectField.SetEnabled(false);
            return objectField;
        };
        listView.columns["id"].bindCell = (e, i) => {
            var item = listView.GetItemDataForIndex<Item>(i);
            var index = item.Id.LastIndexOf('/');
            e.Q<TextElement>().text = index != -1 ? item.Id[(index + 1)..] : item.Id;
        };
        listView.columns["visualTree"].bindCell = (e, i) => {
            var item = listView.GetItemDataForIndex<Item>(i);
            var objectField = e.Q<ObjectField>();
            objectField.value = item.VisualTree;
            objectField.style.display = item.VisualTree != null ? DisplayStyle.Flex : DisplayStyle.None;
        };
        root.Add(listView);
        root.Add(new Button(() => {
            var window = EditorWindow.GetWindow<LocalizationTablesWindow>();
            var table = ((LocalizedAssetVisualTreeCollectionAsset)target).Table;
            var collection = LocalizationEditorSettings.GetAssetTableCollection(table.TableReference);
            window.EditCollection(collection);
        }) { text = "Open Editor" });
        return root;
    }

    IList<TreeViewItemData<Item>> GetRootItems() {
        var table = ((LocalizedAssetVisualTreeCollectionAsset)target).Table;
        if (table.IsEmpty) return Array.Empty<TreeViewItemData<Item>>();
        var database = LocalizationSettings.Instance.GetAssetDatabase();
        var id = 0;
        var entries = LocalizationEditorSettings.GetAssetTableCollection(table.TableReference).SharedData.Entries
            .OrderBy(x => x.Key).Select(x => new Item {
                Id = x.Key,
                VisualTree = database.GetLocalizedAsset<VisualTreeAsset>(table.TableReference, x.Key)
            }).GroupBy(x => {
                var index = x.Id.LastIndexOf('/');
                return index != -1 ? x.Id[..index] : x.Id;
            }).Select(x => {
                var first = x.First();
                var groupItem = first.Id == x.Key
                    ? first with { Id = x.Key }
                    : new Item { Id = x.Key, VisualTree = null };
                return new TreeViewItemData<Item>(id++, groupItem,
                    x.Where(y => y.Id != x.Key).Select(y => new TreeViewItemData<Item>(id++, y)).ToList());
            }).ToArray();
        return entries;
    }
}
