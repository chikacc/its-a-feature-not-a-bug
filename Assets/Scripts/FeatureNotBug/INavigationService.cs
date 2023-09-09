﻿using UnityEngine.UIElements;

namespace FeatureNotBug; 

public interface INavigationService {
    int Count { get; }
    void Push(string next);
    void Push(LocalizedVisualTreeAsset next);
    void Push(VisualTreeAsset next);
    void Push(VisualElement next);
    void Pop();
    void Clear();
}
