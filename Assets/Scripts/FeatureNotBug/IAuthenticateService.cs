using System.Threading;
using Cysharp.Threading.Tasks;

namespace FeatureNotBug;

public interface IAuthenticateService {
    string PlayerId { get; }

    UniTask<string> SignIn(CancellationToken cancellationToken);
}
