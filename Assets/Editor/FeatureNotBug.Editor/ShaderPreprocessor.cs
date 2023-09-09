using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Rendering;
using UnityEngine;

namespace FeatureNotBug.Editor;

public sealed class ShaderPreProcessor : IPreprocessShaders {
    static readonly List<ShaderVariantCollection> collections = new();

    public int callbackOrder => 0;

    static ShaderPreProcessor() {
        collections.Clear();
        var guids = AssetDatabase.FindAssets($"t:{nameof(ShaderVariantCollection)}");
        for (var i = 0; i < guids.Length; ++i) {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            var collection = AssetDatabase.LoadAssetAtPath<ShaderVariantCollection>(path);
            collections.Add(collection);
        }
    }

    public void OnProcessShader(Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> data) {
        for (var i = 0; i < data.Count; ++i) {
            var compilerData = data[i];
            var keywords = compilerData.shaderKeywordSet.GetShaderKeywords().Select(x => x.name).ToArray();
            try {
                var shaderVariant = new ShaderVariantCollection.ShaderVariant(shader, snippet.passType, keywords);
                for (var j = 0; j < collections.Count; ++j)
                    if (!collections[j].Contains(shaderVariant)) {
                        data.RemoveAt(i);
                        --i;
                        break;
                    }
            } catch (Exception e) {
                Debug.LogWarningFormat("Failed to create shader variant for {0} with keywords {1}.\n{2}", shader.name, string.Join(", ", keywords), e);
            }
        }
    }
}
