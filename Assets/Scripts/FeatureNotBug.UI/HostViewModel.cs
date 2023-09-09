using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Properties;
using UnityEngine.UIElements;

namespace FeatureNotBug.UI; 

public sealed class HostViewModel : INotifyBindablePropertyChanged {
    readonly IHostService _hostService;

    string _joinCode = string.Empty;

    public HostViewModel(IHostService hostService) {
        _hostService = hostService;
    }

    [CreateProperty]
    public string JoinCode => _joinCode;

    public event EventHandler<BindablePropertyChangedEventArgs>? propertyChanged;

    void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = "") {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        propertyChanged?.Invoke(this, new BindablePropertyChangedEventArgs(propertyName));
    }

    public async UniTask<bool> Start(CancellationToken cancellationToken) {
        if (!await _hostService.Start(cancellationToken)) return false;
        Set(ref _joinCode, _hostService.JoinCode);
        return true;
    }
}