using System.Threading;
using System.Windows.Input;
using Cysharp.Threading.Tasks;
using Unity.Properties;

namespace FeatureNotBug.UI; 

public sealed class AuthenticateViewModel {
    readonly IAuthenticateService _authenticateService;

    public AuthenticateViewModel(IAuthenticateService authenticateService) {
        _authenticateService = authenticateService;
        SignInCommand = new RelayCommand(_ =>
            UniTask.Void(static async x => await x._authenticateService.SignIn(CancellationToken.None), this));
    }

    [CreateProperty]
    public string PlayerId => _authenticateService.PlayerId;

    [CreateProperty]
    public ICommand SignInCommand { get; set; }
}