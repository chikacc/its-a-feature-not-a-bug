using System.Threading;
using Cysharp.Threading.Tasks;

namespace FeatureNotBug;

public interface IHostService {
    string JoinCode { get; }

    UniTask<bool> Start(CancellationToken cancellationToken);
    void Shutdown();
}
