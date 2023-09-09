using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FeatureNotBug.UI.Events;
using FeatureNotBug.UI.Views;
using MessagePipe;

namespace FeatureNotBug.UI.Presenters;

public sealed class LobbyPresenter : IDisposable {
    readonly LobbyView _view;
    readonly IAsyncPublisher<OpenHostViewEvent> _openCreateRoomPublisher;
    readonly IAsyncPublisher<OpenClientViewEvent> _openJoinRoomPublisher;
    readonly IDisposable _disposable;

    public LobbyPresenter(LobbyView view, IAsyncPublisher<OpenHostViewEvent> openCreateRoomPublisher,
        IAsyncPublisher<OpenClientViewEvent> openJoinRoomPublisher, IAsyncSubscriber<OpenLobbyViewEvent> openSubscriber) {
        _view = view;
        _openCreateRoomPublisher = openCreateRoomPublisher;
        _openJoinRoomPublisher = openJoinRoomPublisher;
        _view.CreateRoom += OnCreateRoom;
        _view.JoinRoom += OnJoinRoom;
        _disposable = openSubscriber.Subscribe(OnOpen);
    }

    public void Dispose() {
        _disposable.Dispose();
    }

    void OnCreateRoom() {
        _view.Hide();
        _openCreateRoomPublisher.Publish(new OpenHostViewEvent());
    }

    void OnJoinRoom() {
        _view.Hide();
        _openJoinRoomPublisher.Publish(new OpenClientViewEvent());
    }

    UniTask OnOpen(OpenLobbyViewEvent evt, CancellationToken cancellationToken) {
        _view.Show();
        return UniTask.CompletedTask;
    }
}
