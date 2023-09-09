using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI.Editor.FeatureNotBug.UI.Editor;

[CustomEditor(typeof(DefaultVisualTreeCollectionAsset))]
public sealed class DefaultVisualTreeCollectionAssetEditor : UnityEditor.Editor {
    public override VisualElement CreateInspectorGUI() {
        var root = new VisualElement();
        var listView = new MultiColumnListView {
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
            selectionType = SelectionType.Multiple,
            showBoundCollectionSize = false,
            showAddRemoveFooter = true,
            itemsSource = ((DefaultVisualTreeCollectionAsset)target).Items,
            onAdd = _ => {
                var property = serializedObject.FindProperty(nameof(DefaultVisualTreeCollectionAsset.Items));
                var index = property.arraySize;
                property.InsertArrayElementAtIndex(index);
                if (index == 0) {
                    serializedObject.ApplyModifiedProperties();
                    return;
                }

                var newElement = property.GetArrayElementAtIndex(index);
                var previousElement = property.GetArrayElementAtIndex(index - 1);
                newElement.FindPropertyRelative(nameof(DefaultVisualTreeCollectionAsset.Item.Id))
                    .stringValue = previousElement.FindPropertyRelative(nameof(DefaultVisualTreeCollectionAsset.Item.Id))
                    .stringValue;
                newElement.FindPropertyRelative(nameof(DefaultVisualTreeCollectionAsset.Item.VisualTree))
                        .objectReferenceValue =
                    previousElement.FindPropertyRelative(nameof(DefaultVisualTreeCollectionAsset.Item.VisualTree))
                        .objectReferenceValue;
                serializedObject.ApplyModifiedProperties();
            }
        };
        listView.columns.Add(new Column { name = "id", title = "Id", stretchable = true });
        listView.columns.Add(new Column { name = "visualTree", title = "Visual Tree", stretchable = true });
        listView.columns["id"].makeCell = () => new TextField();
        listView.columns["visualTree"].makeCell = () => new ObjectField { objectType = typeof(VisualTreeAsset) };
        listView.columns["id"].bindCell = (e, i) =>
            ((IBindable)e).BindProperty(serializedObject.FindProperty(nameof(DefaultVisualTreeCollectionAsset.Items))
                .GetArrayElementAtIndex(i).FindPropertyRelative(nameof(DefaultVisualTreeCollectionAsset.Item.Id)));
        listView.columns["visualTree"].bindCell = (e, i) => {
            var objectField = (ObjectField)e;
            objectField.BindProperty(serializedObject.FindProperty(nameof(DefaultVisualTreeCollectionAsset.Items))
                .GetArrayElementAtIndex(i).FindPropertyRelative(nameof(DefaultVisualTreeCollectionAsset.Item.VisualTree)));
            objectField.RegisterValueChangedCallback(evt => {
                if (evt.newValue == null) return;
                var idProperty = serializedObject.FindProperty(nameof(DefaultVisualTreeCollectionAsset.Items))
                    .GetArrayElementAtIndex(i).FindPropertyRelative(nameof(DefaultVisualTreeCollectionAsset.Item.Id));
                if (!string.IsNullOrEmpty(idProperty.stringValue)) return;
                idProperty.stringValue = evt.newValue.name;
                serializedObject.ApplyModifiedProperties();
            });
        };
        root.Add(listView);
        return root;
    }
}
