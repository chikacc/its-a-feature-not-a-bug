using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FeatureNotBug;

public static class PropertyInfoContainer {
    static readonly Dictionary<(Type, string), PropertyInfo> _caches = new();

#if UNITY_EDITOR
    [InitializeOnEnterPlayMode]
    static void OnEnterPlayModeInEditor(EnterPlayModeOptions options) {
        _caches.Clear();
    }
#endif

    public static PropertyInfo? Get(Type type, string name) {
        if (_caches.TryGetValue((type, name), out var cache) && cache != null) return cache;
        cache = type.GetProperty(name);
        _caches[(type, name)] = cache;
        return cache;
    }

    public static bool TryGet(Type type, string name, [NotNullWhen(true)] out PropertyInfo? cache) {
        if (_caches.TryGetValue((type, name), out cache) && cache != null) return true;
        cache = type.GetProperty(name);
        _caches[(type, name)] = cache;
        return cache is not null;
    }

    public static PropertyInfo? Get(Type type, string name, BindingFlags bindingAttr) {
        if (_caches.TryGetValue((type, name), out var cache) && cache != null) return cache;
        cache = type.GetProperty(name, bindingAttr);
        _caches[(type, name)] = cache;
        return cache;
    }

    public static bool TryGet(Type type, string name, BindingFlags bindingAttr,
        [NotNullWhen(true)] out PropertyInfo? cache) {
        if (_caches.TryGetValue((type, name), out cache) && cache != null) return true;
        cache = type.GetProperty(name, bindingAttr);
        _caches[(type, name)] = cache;
        return cache is not null;
    }
}
