﻿using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Localization;

namespace FeatureNotBug.UI; 

public interface ILocalizationService {
    Locale CurrentLocale { get; }
    event Action<Locale> LocaleChanged;
    UniTask<Locale> NextLocale(CancellationToken cancellationToken);
}
