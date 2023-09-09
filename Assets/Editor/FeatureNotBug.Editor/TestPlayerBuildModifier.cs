using System.IO;
using FeatureNotBug.Editor;
using UnityEditor;
using UnityEditor.TestTools;

[assembly: TestPlayerBuildModifier(typeof(TestPlayerBuildModifier))]

namespace FeatureNotBug.Editor; 

public sealed class TestPlayerBuildModifier : ITestPlayerBuildModifier {
    public static bool Modified { get; private set; }

    [InitializeOnEnterPlayMode]
    static void OnEnterPlayModeInEditor(EnterPlayModeOptions options) {
        Modified = false;
    }

    public BuildPlayerOptions ModifyOptions(BuildPlayerOptions playerOptions) {
        var folder = Path.GetFullPath("Builds/PlayerWithTests");
        var name = Path.GetFileName(playerOptions.locationPathName);
        playerOptions.locationPathName = Path.Combine(folder, name);
        Modified = true;
        return playerOptions;
    }
}