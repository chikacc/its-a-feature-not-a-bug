using System.IO;
using UnityEditor;

// ReSharper disable PartialTypeWithSinglePart

namespace EditorTools; 

public static partial class AssetDatabaseUtility {
    public static void CreateDirectory(string pathName) {
        var targetFolder = pathName.StartsWith("Assets/") ? pathName : "Assets/" + pathName;
        var breadcrumb = targetFolder.Split('/');
        var parentFolder = breadcrumb[0];
        for (var i = 1; i < breadcrumb.Length; ++i) {
            var folderName = breadcrumb[i];
            var folder = Path.Combine(parentFolder, folderName);
            if (!AssetDatabase.IsValidFolder(folder)) AssetDatabase.CreateFolder(parentFolder, folderName);
            parentFolder = folder;
        }
    }

    public static void DeleteDirectory(string pathName) {
        var targetFolder = pathName.StartsWith("Assets/") ? pathName : "Assets/" + pathName;
        AssetDatabase.DeleteAsset(targetFolder);
    }
}
