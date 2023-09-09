using System.Threading;
using System.Windows.Input;
using Cysharp.Threading.Tasks;
using Unity.Properties;
using UnityEngine;

namespace FeatureNotBug.UI; 

public sealed class LobbyViewModel {
    readonly IAuthenticateService _authenticateService;
    readonly IHostService _hostService;
    readonly IClientService _clientService;

    public LobbyViewModel(IAuthenticateService authenticateService, IHostService hostService, IClientService clientService, INavigationService navigationService) {
        _authenticateService = authenticateService;
        _hostService = hostService;
        _clientService = clientService;
        SignInCommand = new RelayCommand(_ =>
            UniTask.Void(static async x => await x._authenticateService.SignIn(CancellationToken.None), this));
        StartHostCommand = new RelayCommand(_ =>
            UniTask.Void(static async x => {
                await x._hostService.Start(CancellationToken.None);
                Debug.LogFormat("Join code: {0}", x._hostService.JoinCode);
            }, this));
        StartClientCommand = new RelayCommand(_ =>
            UniTask.Void(static async x => await x._clientService.Start(x.JoinCode, CancellationToken.None), this));
        StartReviewCommand = new RelayCommand(_ => navigationService.Push("Review"));
    }

    [CreateProperty]
    public ICommand SignInCommand { get; private set; }

    [CreateProperty]
    public ICommand StartHostCommand { get; private set; }

    [CreateProperty]
    public string JoinCode { get; set; } = string.Empty;

    [CreateProperty]
    public ICommand StartClientCommand { get; private set; }

    [CreateProperty]
    public ICommand StartReviewCommand { get; private set; }
}
