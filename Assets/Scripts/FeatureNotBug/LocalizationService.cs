using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace FeatureNotBug;

public sealed class LocalizationService : ILocalizationService, IDisposable {
    readonly LocalizationSettings _localizationSettings;

    public LocalizationService(LocalizationSettings localizationSettings) {
        _localizationSettings = localizationSettings;
        CurrentLocale = _localizationSettings.GetSelectedLocale();
        _localizationSettings.OnSelectedLocaleChanged += OnSelectedLocaleChanged;
    }

    public Locale CurrentLocale { get; private set; }

    public event Action<Locale> LocaleChanged {
        add => _localizationSettings.OnSelectedLocaleChanged += value;
        remove => _localizationSettings.OnSelectedLocaleChanged -= value;
    }

    public void Dispose() {
        _localizationSettings.OnSelectedLocaleChanged -= OnSelectedLocaleChanged;
    }

    public UniTask<Locale> GetSelectedLocale(CancellationToken cancellationToken) {
        return _localizationSettings.GetSelectedLocaleAsync().WithCancellation(cancellationToken);
    }

    public async UniTask<Locale> NextLocale(CancellationToken cancellationToken) {
        var locale = await _localizationSettings.GetSelectedLocaleAsync().WithCancellation(cancellationToken);
        var availableLocales = _localizationSettings.GetAvailableLocales();
        var index = availableLocales.Locales.IndexOf(locale);
        var nextIndex = (index + 1) % availableLocales.Locales.Count;
        var newLocale = availableLocales.Locales[nextIndex];
        _localizationSettings.SetSelectedLocale(newLocale);
        return newLocale;
    }

    void OnSelectedLocaleChanged(Locale locale) {
        CurrentLocale = locale;
    }
}
