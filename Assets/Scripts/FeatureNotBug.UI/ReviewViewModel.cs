using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Unity.Properties;
using UnityEngine;

namespace FeatureNotBug.UI;

public sealed class ReviewViewModel {
    readonly INavigationService _navigationService;
    readonly LocalizedReviewPhaseAsset _initialPhaseReference;
    float _elapsedTime;

    public ReviewViewModel(INavigationService navigationService, LocalizedReviewPhaseAsset initialPhaseReference) {
        _navigationService = navigationService;
        _initialPhaseReference = initialPhaseReference;
        NextCommand = new RelayCommand(_ => OnNext());
        InternalNext(initialPhaseReference);
    }

    public ReviewPhaseAsset Current { get; private set; } = null!;

    [CreateProperty]
    public string Title { get; private set; } = string.Empty;

    [CreateProperty]
    public Color AccentColor => Current.AccentColor;

    [CreateProperty]
    public string[] Descriptions { get; private set; } = Array.Empty<string>();

    [CreateProperty]
    public bool IsTimerVisible => Current.TimerDuration > 0;

    [CreateProperty]
    public TimeSpan RemainingTime => TimeSpan.FromSeconds(Current.TimerDuration - _elapsedTime);

    [CreateProperty]
    public float Progress =>
        Current.TimerDuration > 0 ? (float)RemainingTime.TotalSeconds / Current.TimerDuration * 100f : 0;

    [CreateProperty]
    public string Next { get; private set; } = string.Empty;

    [CreateProperty]
    public ICommand NextCommand { get; private set; }

    void InternalNext(LocalizedReviewPhaseAsset next) {
        Current = next.LoadAsset();
        _elapsedTime = 0f;
        Current.Title.StringChanged += OnTitleChanged;
        Current.Description.StringChanged += OnDescriptionChanged;
        Current.Next.StringChanged += OnNextChanged;
    }

    void OnNext() {
        Current.Title.StringChanged -= OnTitleChanged;
        Current.Description.StringChanged -= OnDescriptionChanged;
        Current.Next.StringChanged -= OnNextChanged;
        if (Current.NextReference.IsEmpty) {
            _navigationService.Clear();
            _navigationService.Push("Start");
            InternalNext(_initialPhaseReference);
            return;
        }

        InternalNext(Current.NextReference);
    }

    void OnTitleChanged(string value) {
        Title = value;
    }

    void OnDescriptionChanged(string value) {
        var descriptions = new List<string>();
        var lines = value.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var builder = new StringBuilder();
        builder.Append(lines[0].TrimStart('-').Trim());
        for (var i = 1; i < lines.Length; ++i) {
            if (lines[i] is ['-', ' ', .. var content]) {
                descriptions.Add(builder.ToString());
                builder.Clear();
                builder.Append(content.Trim());
                continue;
            }

            builder.Append('\n');
            builder.Append(lines[i].Trim());
        }

        descriptions.Add(builder.ToString());
        Descriptions = descriptions.ToArray();
    }

    void OnNextChanged(string value) {
        Next = value;
    }
}
