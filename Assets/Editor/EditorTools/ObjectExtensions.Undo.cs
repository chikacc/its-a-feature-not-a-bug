using UnityEditor;
using UnityEngine;

// ReSharper disable PartialTypeWithSinglePart

namespace EditorTools; 

public static partial class ObjectExtensions {
    public static T RegisterCreatedObjectUndo<T>(this T objectToUndo, string name) where T : Object {
        Undo.RegisterCreatedObjectUndo(objectToUndo, name);
        return objectToUndo;
    }
}
