using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FeatureNotBug.UI.Views;

namespace FeatureNotBug.UI.Presenters;

public sealed class LanguagePresenter : IDisposable {
    readonly ILocalizationService _localizationService;
    readonly LanguageView _view;

    public LanguagePresenter(ILocalizationService localizationService, LanguageView view) {
        _localizationService = localizationService;
        _view = view;
        _view.NextLocale += OnNextLocale;
    }

    public void Dispose() {
        _view.NextLocale -= OnNextLocale;
    }

    void OnNextLocale() {
        UniTask.Void(static async x => await x._localizationService.NextLocale(CancellationToken.None), this);
    }
}
