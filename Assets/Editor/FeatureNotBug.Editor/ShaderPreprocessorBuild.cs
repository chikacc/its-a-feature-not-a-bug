using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace FeatureNotBug.Editor;

public sealed class ShaderPreprocessorBuild : IPreprocessBuildWithReport {
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report) {
        Debug.Log(report.summary.result.ToString());
    }
}
