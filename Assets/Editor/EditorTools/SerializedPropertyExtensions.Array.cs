using UnityEditor;

// ReSharper disable PartialTypeWithSinglePart

namespace EditorTools; 

public static partial class SerializedPropertyExtensions {
    public static SerializedProperty AddArrayElement(this SerializedProperty property) {
        property.InsertArrayElementAtIndex(property.arraySize);
        return property.GetArrayElementAtIndex(property.arraySize - 1);
    }

    public static SerializedProperty InsertArrayElement(this SerializedProperty property, int index) {
        property.InsertArrayElementAtIndex(index);
        return property.GetArrayElementAtIndex(index);
    }
}
