using System;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI.Views;

public sealed class LobbyView {
    readonly VisualElement _root;
    readonly Button _createRoom;
    readonly Button _joinRoom;

    public LobbyView(VisualElement root) {
        _root = root;
        _createRoom = _root.Q<Button>("create-room");
        _joinRoom = _root.Q<Button>("join-room");
    }

    public event Action CreateRoom { add => _createRoom.clicked += value; remove => _createRoom.clicked -= value; }

    public event Action JoinRoom { add => _joinRoom.clicked += value; remove => _joinRoom.clicked -= value; }

    public void Show() {
        _root.style.display = DisplayStyle.Flex;
    }

    public void Hide() {
        _root.style.display = DisplayStyle.None;
    }
}
