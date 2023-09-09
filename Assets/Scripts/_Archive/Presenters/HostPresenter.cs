using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FeatureNotBug.UI.Events;
using FeatureNotBug.UI.Views;
using MessagePipe;
using Unity.Logging;

namespace FeatureNotBug.UI.Presenters;

public sealed class HostPresenter : IDisposable {
    readonly HostView _view;
    readonly IHostService _hostService;
    readonly IQrCodeService _qrCodeService;
    readonly IAsyncPublisher<OpenLobbyViewEvent> _backPublisher;
    readonly IDisposable _disposable;

    public HostPresenter(HostView view, IHostService hostService,
        IQrCodeService qrCodeService, IAsyncPublisher<OpenLobbyViewEvent> backPublisher,
        IAsyncSubscriber<OpenHostViewEvent> openSubscriber) {
        _view = view;
        _hostService = hostService;
        _qrCodeService = qrCodeService;
        _backPublisher = backPublisher;
        _view.Back += OnBack;
        _disposable = openSubscriber.Subscribe(OnOpen);
    }

    public void Dispose() {
        _view.Back -= OnBack;
        _disposable.Dispose();
    }

    void OnBack() {
        _view.Hide();
        _hostService.Shutdown();
        _backPublisher.Publish(new OpenLobbyViewEvent());
    }

    async UniTask OnOpen(OpenHostViewEvent evt, CancellationToken cancellationToken) {
        _view.Show();
        if (!await _hostService.Start(cancellationToken)) {
            Log.Error("Failed to start host service");
            return;
        }

        Log.Info("Host service started");
        var contents = _hostService.JoinCode;
        var texture = _qrCodeService.Generate(contents);
        _view.SetQrCode(texture);
    }
}
