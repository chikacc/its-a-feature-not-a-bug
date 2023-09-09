using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Logging;
using Unity.Services.Authentication;
using Unity.Services.Core;

namespace FeatureNotBug;

public sealed class AuthenticateService : IAuthenticateService {
    IAuthenticationService? _authenticationService;

    public string PlayerId => _authenticationService.PlayerId;

    public async UniTask<string> SignIn(CancellationToken cancellationToken) {
        if (_authenticationService is null) {
            await UnityServices.InitializeAsync().AsUniTask().AttachExternalCancellation(cancellationToken);
            _authenticationService = AuthenticationService.Instance;
        }

        if (_authenticationService.IsSignedIn) {
            Log.Info("Already signed in as {PlayerId}", PlayerId);
            return PlayerId;
        }

        await _authenticationService.SignInAnonymouslyAsync().AsUniTask().AttachExternalCancellation(cancellationToken);
        Log.Info("Signed in as {PlayerId}", PlayerId);
        return PlayerId;
    }
}
