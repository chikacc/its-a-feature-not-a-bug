using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FeatureNotBug.UI.Events;
using FeatureNotBug.UI.Views;
using MessagePipe;

namespace FeatureNotBug.UI.Presenters;

public sealed class PressStartPresenter : IDisposable {
    readonly IAuthenticateService _authenticateService;
    readonly PressStartView _view;
    readonly IAsyncPublisher<OpenLobbyViewEvent> _openLobbyPublisher;

    public PressStartPresenter(IAuthenticateService authenticateService, PressStartView view, IAsyncPublisher<OpenLobbyViewEvent> openLobbyPublisher) {
        _authenticateService = authenticateService;
        _view = view;
        _openLobbyPublisher = openLobbyPublisher;
        _view.PressStart += OnPressStart;
    }

    public void Dispose() {
        _view.PressStart -= OnPressStart;
    }

    void OnPressStart() {
        UniTask.Void(static async x => {
            x._view.Hide();
            await x._authenticateService.SignIn(CancellationToken.None);
            await x._openLobbyPublisher.PublishAsync(new OpenLobbyViewEvent());
        }, this);
    }
}
