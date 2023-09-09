using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FeatureNotBug.UI.Events;
using FeatureNotBug.UI.Views;
using MessagePipe;
using Unity.Logging;
using UnityEngine;

namespace FeatureNotBug.UI.Presenters;

public sealed class ClientPresenter : IDisposable {
    readonly ClientView _view;
    readonly IClientService _clientService;
    readonly IQrCodeService _qrCodeService;
    readonly IWebCamService _webCamService;
    readonly IAsyncPublisher<OpenLobbyViewEvent> _openLobbyPublisher;
    readonly IDisposable _disposable;

    public ClientPresenter(ClientView view, IClientService clientService, IQrCodeService qrCodeService,
        IWebCamService webCamService, IAsyncPublisher<OpenLobbyViewEvent> openLobbyPublisher,
        IAsyncSubscriber<OpenClientViewEvent> openSubscriber) {
        _view = view;
        _clientService = clientService;
        _qrCodeService = qrCodeService;
        _webCamService = webCamService;
        _openLobbyPublisher = openLobbyPublisher;
        _view.Back += OnBack;
        _disposable = openSubscriber.Subscribe(OnOpen);
    }

    public void Dispose() {
        _view.Back -= OnBack;
        _disposable.Dispose();
    }

    void OnBack() {
        _view.Hide();
        _clientService.Shutdown();
        _openLobbyPublisher.Publish(new OpenLobbyViewEvent());
    }

    async UniTask OnOpen(OpenClientViewEvent evt, CancellationToken cancellationToken) {
        _view.Show();
        if (!await _webCamService.Open(cancellationToken)) {
            Log.Info("WebCam has not been opened.");
            return;
        }

        _view.SetWebCam(_webCamService.Texture);
        while (!cancellationToken.IsCancellationRequested) {
            if (!_webCamService.IsPlaying()) {
                Log.Info("WebCam has been closed.");
                break;
            }

            if (await TryJoin()) break;
            await UniTask.Yield();
        }
    }

    async UniTask<bool> TryJoin() {
        var colors = _webCamService.GetPixels32();
        if (!TryParseJoinCode(colors, out var joinCode)) return false;
        if (!await _clientService.Start(joinCode)) {
            _webCamService.Close();
            return false;
        }

        Log.Info("Client has been started.");
        return true;
    }

    bool TryParseJoinCode(Color32[] colors, out string joinCode) {
        if (!_qrCodeService.TryParse(colors, _webCamService.Width, _webCamService.Height, out var contents)) {
            joinCode = null!;
            return false;
        }

        joinCode = contents;
        return true;
    }
}
