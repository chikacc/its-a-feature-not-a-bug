using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI.Views;

public sealed class ClientView {
    readonly VisualElement _root;
    readonly Image _webCam;
    readonly Button _back;

    public ClientView(VisualElement root) {
        _root = root;
        _webCam = root.Q<Image>("web-cam");
        _back = root.Q<Button>("back");
    }

    public event Action Back { add => _back.clicked += value; remove => _back.clicked -= value; }

    public void Show() {
        _root.style.display = DisplayStyle.Flex;
    }

    public void Hide() {
        _root.style.display = DisplayStyle.None;
    }

    public void SetWebCam(Texture? texture) {
        _webCam.image = texture;
    }
}
