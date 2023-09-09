using System.Windows.Input;
using Cysharp.Threading.Tasks;
using Unity.Properties;
using UnityEngine;

namespace FeatureNotBug.UI; 

public sealed class StartViewModel {
    public StartViewModel(INavigationService navigationService) {
        StartCommand = new RelayCommand(_ => navigationService.Push("Review"));
    }

    [CreateProperty]
    public ICommand StartCommand { get; private set; }
}
