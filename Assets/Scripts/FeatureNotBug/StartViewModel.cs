using System.Windows.Input;
using Unity.Properties;

namespace FeatureNotBug; 

public sealed class StartViewModel {
    public StartViewModel(INavigationService navigationService) {
        StartCommand = new RelayCommand(_ => navigationService.Push("Review"));
    }

    [CreateProperty]
    public ICommand StartCommand { get; private set; }
}
