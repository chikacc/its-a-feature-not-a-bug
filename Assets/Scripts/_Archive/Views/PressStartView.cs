using System;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI.Views;

public sealed class PressStartView {
    readonly VisualElement _root;
    readonly Button _pressStart;

    public PressStartView(VisualElement root) {
        _root = root;
        _pressStart = root.Q<Button>("press-start");
    }

    public event Action PressStart { add => _pressStart.clicked += value; remove => _pressStart.clicked -= value; }

    public void Show() {
        _root.style.display = DisplayStyle.Flex;
    }

    public void Hide() {
        _root.style.display = DisplayStyle.None;
    }
}
