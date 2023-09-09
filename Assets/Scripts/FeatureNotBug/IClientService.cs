using System.Threading;
using Cysharp.Threading.Tasks;

namespace FeatureNotBug;

public interface IClientService {
    string JoinCode { get; }
    UniTask<bool> Start(string joinCode, CancellationToken cancellationToken);
    void Shutdown();
}
