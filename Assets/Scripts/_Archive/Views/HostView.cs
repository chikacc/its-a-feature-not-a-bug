using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI.Views;

public sealed class HostView {
    readonly VisualElement _root;
    readonly Image _qrCode;
    readonly Button _back;

    public HostView(VisualElement root) {
        _root = root;
        _qrCode = root.Q<Image>("qr-code");
        _back = root.Q<Button>("back");
    }

    public event Action Back { add => _back.clicked += value; remove => _back.clicked -= value; }

    public void Show() {
        _root.style.display = DisplayStyle.Flex;
    }

    public void Hide() {
        _root.style.display = DisplayStyle.None;
    }

    public void SetQrCode(Texture? texture) {
        _qrCode.image = texture;
    }
}
