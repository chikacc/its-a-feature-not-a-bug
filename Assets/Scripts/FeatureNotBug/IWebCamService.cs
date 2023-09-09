using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FeatureNotBug;

public interface IWebCamService {
    int RequestedFps { get; set; }
    WebCamTexture? Texture { get; }
    int Width { get; }
    int Height { get; }
    bool IsPlaying();
    UniTask<bool> Open(CancellationToken cancellationToken);
    void Close();
    Color32[] GetPixels32();
}
