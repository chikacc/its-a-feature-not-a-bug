using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FeatureNotBug;

public sealed class WebCamService : IWebCamService {
    Color32[] _colors = Array.Empty<Color32>();

    public int RequestedFps { get; set; } = 30;
    public WebCamTexture? Texture { get; private set; }
    public int Width => Texture != null ? Texture.width : 0;
    public int Height => Texture != null ? Texture.height : 0;

    void OnApplicationPause(bool pause) {
        if (Texture == null) return;
        if (pause) Texture.Pause();
        else Texture.Play();
    }

    public bool IsPlaying() {
        if (Texture == null) return false;
        return Texture.isPlaying;
    }

    public async UniTask<bool> Open(CancellationToken cancellationToken) {
        if (IsPlaying()) Close();
        await Application.RequestUserAuthorization(UserAuthorization.WebCam).WithCancellation(cancellationToken);
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam)) return false;
        var devices = WebCamTexture.devices;
        if (devices.Length == 0) return false;
        var device = devices[0];
        if (device.isFrontFacing)
            for (var i = 1; i < devices.Length; ++i)
                if (!devices[i].isFrontFacing) {
                    device = devices[i];
                    break;
                }

        Texture = new WebCamTexture {
            deviceName = device.name,
            requestedWidth = Screen.width,
            requestedHeight = Screen.height,
            requestedFPS = RequestedFps,
            wrapMode = TextureWrapMode.Repeat
        };
        Texture.Play();
        return true;
    }

    public void Close() {
        if (Texture == null) return;
        Texture.Stop();
        Texture = null;
    }

    public Color32[] GetPixels32() {
        if (Texture == null) return Array.Empty<Color32>();
        if (_colors.Length < Texture.width * Texture.height)
            _colors = new Color32[Texture.width * Texture.height];
        return Texture.GetPixels32(_colors);
    }
}
