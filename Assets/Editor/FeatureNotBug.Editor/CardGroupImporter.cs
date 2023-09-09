using System;
using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

// ReSharper disable ArrangeNamespaceBody

namespace FeatureNotBug.Editor {
    [ScriptedImporter(Version, CardGroupAsset.Extension)]
    public sealed class CardGroupImporter : ScriptedImporter {
        const int Version = 1;

        public override void OnImportAsset(AssetImportContext ctx) {
            if (!TryRead(ctx, out var text)) return;
            if (!TryLoad(ctx, text, out var asset)) return;
            asset.name = Path.GetFileNameWithoutExtension(ctx.assetPath);
            ctx.AddObjectToAsset("<root>", asset);
            ctx.SetMainObject(asset);
        }

        static bool TryRead(AssetImportContext ctx, out string text) {
            try {
                text = File.ReadAllText(ctx.assetPath);
                return true;
            } catch (Exception e) {
                ctx.LogImportError($"Failed to read {ctx.assetPath}: {e}");
                throw;
            }
        }

        static bool TryLoad(AssetImportContext ctx, string text, out CardGroupAsset asset) {
            asset = ScriptableObject.CreateInstance<CardGroupAsset>();
            try {
                var lines = text.Split('\n');
                asset.Cards = new Card[lines.Length];
                for (var i = 0; i < lines.Length; i++) {
                    var line = lines[i];
                    var fields = line.Split(',');
                    if (!int.TryParse(fields[0], out var id)) continue;
                    asset.Cards[i] = new Card { Id = id };
                }

                return true;
            } catch (Exception e) {
                ctx.LogImportError($"Failed to load {ctx.assetPath}: {e}");
                DestroyImmediate(asset);
                throw;
            }
        }
    }
}
