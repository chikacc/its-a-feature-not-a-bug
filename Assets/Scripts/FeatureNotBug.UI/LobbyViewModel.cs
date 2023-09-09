using System.Windows.Input;
using Unity.Properties;

namespace FeatureNotBug.UI;

public sealed class LobbyViewModel {
    public LobbyViewModel(INavigationService navigationService) {
        BackCommand = new RelayCommand(_ => navigationService.Pop());
    }

    [CreateProperty]
    public ICommand BackCommand { get; private set; }
}
