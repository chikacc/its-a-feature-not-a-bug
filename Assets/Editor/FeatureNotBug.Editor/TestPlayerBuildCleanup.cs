using System;
using System.Linq;
using FeatureNotBug.Editor;
using UnityEditor;
using UnityEngine.TestTools;

[assembly: PostBuildCleanup(typeof(TestPlayerBuildCleanup))]

namespace FeatureNotBug.Editor; 

public sealed class TestPlayerBuildCleanup : IPostBuildCleanup {
    public void Cleanup() {
        if (!TestPlayerBuildModifier.Modified) return;
        if (!Environment.GetCommandLineArgs().Contains("-runTests")) return;
        EditorApplication.update += () => EditorApplication.Exit(0);
    }
}
