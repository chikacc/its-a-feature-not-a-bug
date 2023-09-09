using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FeatureNotBug;

public static class EventInfoContainer {
    static readonly Dictionary<(Type, string), EventInfo> _caches = new();

#if UNITY_EDITOR
    [InitializeOnEnterPlayMode]
    static void OnEnterPlayModeInEditor(EnterPlayModeOptions options) {
        _caches.Clear();
    }
#endif

    public static EventInfo? Get(Type type, string name) {
        if (_caches.TryGetValue((type, name), out var cache)) {
            if (cache is not null) return cache;
            _caches.Remove((type, name));
        }

        cache = type.GetEvent(name);
        _caches.Add((type, name), cache);
        return cache;
    }

    public static bool TryGet(Type type, string name, [NotNullWhen(true)] out EventInfo? cache) {
        if (_caches.TryGetValue((type, name), out cache)) {
            if (cache is not null) return true;
            _caches.Remove((type, name));
        }

        cache = type.GetEvent(name);
        _caches.Add((type, name), cache);
        return cache is not null;
    }
}
