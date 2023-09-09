using System.Threading;
using System.Windows.Input;
using Cysharp.Threading.Tasks;
using Unity.Properties;
using UnityEngine.Localization;

namespace FeatureNotBug.UI;

public sealed class LocalizationViewModel {
    readonly ILocalizationService _localizationService;

    public LocalizationViewModel(ILocalizationService localizationService) {
        _localizationService = localizationService;
        NextLocaleCommand = new RelayCommand(_ =>
            UniTask.Void(static async x => await x._localizationService.NextLocale(CancellationToken.None), this));
    }

    [CreateProperty]
    public Locale CurrentLocale => _localizationService.CurrentLocale;

    [CreateProperty]
    public ICommand NextLocaleCommand { get; set; }
}
