#if UNITY_EDITOR
using UnityEditor;

namespace Unity.Netcode.Unity.Netcode.Runtime {
    public static class NetworkManagerEditor {
        [InitializeOnEnterPlayMode]
        static void OnEnterPlayModeInEditor(EnterPlayModeOptions options) {
            NetworkManager.__rpc_func_table.Clear();
            NetworkManager.__rpc_name_table.Clear();
        }
    }
}

#endif
