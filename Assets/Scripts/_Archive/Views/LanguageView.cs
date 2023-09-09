using System;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI.Views;

public sealed class LanguageView {
    readonly VisualElement _root;
    readonly Button _nextLocale;

    public LanguageView(VisualElement root) {
        _root = root;
        _nextLocale = _root.Q<Button>("next-locale");
    }

    public event Action NextLocale { add => _nextLocale.clicked += value; remove => _nextLocale.clicked -= value; }

    public void Show() {
        _root.style.display = DisplayStyle.Flex;
    }

    public void Hide() {
        _root.style.display = DisplayStyle.None;
    }
}
