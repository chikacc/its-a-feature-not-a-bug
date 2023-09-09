using UnityEditor;
using Object = UnityEngine.Object;

// ReSharper disable PartialTypeWithSinglePart

namespace EditorTools;

public static partial class AssetDatabaseExtensions {
    public static Object[] LoadAllAssetRepresentations(this Object source) {
        var assetPath = AssetDatabase.GetAssetPath(source);
        return AssetDatabase.LoadAllAssetRepresentationsAtPath(assetPath);
    }

    public static void ClearAllAssetRepresentations(this Object source) {
        var assetPath = AssetDatabase.GetAssetPath(source);
        var representations = AssetDatabase.LoadAllAssetRepresentationsAtPath(assetPath);
        foreach (var representation in representations)
            if (representation != null)
                Undo.DestroyObjectImmediate(representation);
    }
}
