using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace FeatureNotBug;

public sealed class AuthenticateService : IAuthenticateService {
    IAuthenticationService? _authenticationService;

    public string PlayerId => _authenticationService?.PlayerId ?? string.Empty;

    public async UniTask<string> SignIn(CancellationToken cancellationToken) {
        if (_authenticationService is null) {
            await UnityServices.InitializeAsync().AsUniTask().AttachExternalCancellation(cancellationToken);
            _authenticationService = AuthenticationService.Instance;
        }

        if (_authenticationService.IsSignedIn) {
            Debug.LogFormat("Already signed in as {0}", PlayerId);
            return PlayerId;
        }

        await _authenticationService.SignInAnonymouslyAsync().AsUniTask().AttachExternalCancellation(cancellationToken);
        Debug.LogFormat("Signed in as {0}", PlayerId);
        return PlayerId;
    }
}
