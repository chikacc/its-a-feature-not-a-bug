using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Cysharp.Threading.Tasks;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.Extensions;
using UnityEngine.UIElements;

namespace FeatureNotBug;

public sealed class ReviewViewModel : IDataSourceViewHashProvider {
    readonly INavigationService _navigationService;
    readonly LocalizedReviewPhaseAsset _initialPhaseReference;

    float _elapsedTime;

    public ReviewViewModel(INavigationService navigationService, LocalizedReviewPhaseAsset initialPhaseReference) {
        _navigationService = navigationService;
        _initialPhaseReference = initialPhaseReference;
        StartTimerCommand = new RelayCommand(_ => OnStartTimer());
        PauseTimerCommand = new RelayCommand(_ => OnPauseTimer());
        ResumeTimerCommand = new RelayCommand(_ => OnResumeTimer());
        SkipTimerCommand = new RelayCommand(_ => OnSkipTimer());
        NextCommand = new RelayCommand(_ => OnNext());
        UniTask.Void(async () => await LoadNext(initialPhaseReference, CancellationToken.None));
    }

    public ReviewPhaseAsset? Current { get; private set; }

    [CreateProperty]
    public string Title { get; private set; } = string.Empty;

    [CreateProperty]
    public Color AccentColor => Current != null ? Current.AccentColor : Color.clear;

    [CreateProperty]
    public bool DescriptionAvailable => Current != null && Descriptions.Count > 0;

    [CreateProperty]
    public IList<string> Descriptions { get; private set; } = new List<string>();

    [CreateProperty]
    public bool TimerAvailable => Current != null && Current.TimerDuration > 0f && _elapsedTime < Current.TimerDuration;

    [CreateProperty]
    public bool TimerRunning { get; private set; }

    [CreateProperty]
    public TimeSpan RemainingTime => TimerAvailable ? TimeSpan.FromSeconds(Current!.TimerDuration - _elapsedTime) : TimeSpan.Zero;

    [CreateProperty]
    public float Progress => TimerAvailable && RemainingTime > TimeSpan.Zero ? (float)RemainingTime.TotalSeconds / Current!.TimerDuration * 100f : 0;

    [CreateProperty]
    public bool StartTimerAvailable => TimerAvailable && !TimerRunning && _elapsedTime < float.Epsilon;

    [CreateProperty]
    public ICommand StartTimerCommand { get; private set; }

    [CreateProperty]
    public bool PauseTimerAvailable => TimerAvailable && TimerRunning && _elapsedTime > 0f;

    [CreateProperty]
    public ICommand PauseTimerCommand { get; private set; }

    [CreateProperty]
    public bool ResumeTimerAvailable => TimerAvailable && !TimerRunning && _elapsedTime > 0f;

    [CreateProperty]
    public ICommand ResumeTimerCommand { get; private set; }

    [CreateProperty]
    public bool SkipTimerAvailable => TimerAvailable;

    [CreateProperty]
    public ICommand SkipTimerCommand { get; private set; }

    [CreateProperty]
    public string Next { get; private set; } = string.Empty;

    [CreateProperty]
    public bool NextAvailable => Current != null && (Current.TimerDuration < float.Epsilon || _elapsedTime >= Current.TimerDuration);

    [CreateProperty]
    public ICommand NextCommand { get; private set; }

    long IDataSourceViewHashProvider.GetViewHashCode() {
        var hash = new HashCode();
        hash.Add(Current);
        hash.Add(Title);
        hash.Add(AccentColor);
        hash.Add(DescriptionAvailable);
        hash.Add(Descriptions);
        hash.Add(TimerAvailable);
        hash.Add(TimerRunning);
        hash.Add(RemainingTime);
        hash.Add(Progress);
        hash.Add(StartTimerAvailable);
        hash.Add(PauseTimerAvailable);
        hash.Add(ResumeTimerAvailable);
        hash.Add(SkipTimerAvailable);
        hash.Add(Next);
        hash.Add(NextAvailable);
        return hash.ToHashCode();
    }

    async UniTask LoadNext(LocalizedReviewPhaseAsset next, CancellationToken cancellationToken) {
        Current = await next.LoadAssetAsync().WithCancellation(cancellationToken);
        _elapsedTime = 0f;
        using var _ = PersistentVariablesSource.UpdateScope();
        var source = LocalizationSettings.StringDatabase.SmartFormatter.GetSourceExtension<PersistentVariablesSource>();
        var title = (LocalizedString)source["review"]["title"];
        title.SetReference(Current.Title.TableReference, Current.Title.TableEntryReference);
        Current.Description.StringChanged += OnDescriptionChanged;
        Current.Next.StringChanged += OnNextChanged;
    }

    void OnStartTimer() {
        UniTask.Void(static async x => {
            try {
                x.TimerRunning = true;
                while (x.TimerRunning && x.Current != null && x._elapsedTime < x.Current.TimerDuration) {
                    await UniTask.Yield();
                    x._elapsedTime += Time.deltaTime;
                }
            } finally {
                x.TimerRunning = false;
            }
        }, this);
    }

    void OnPauseTimer() {
        TimerRunning = false;
    }

    void OnResumeTimer() {
        UniTask.Void(static async x => {
            try {
                x.TimerRunning = true;
                while (x.TimerRunning && x.Current != null && x._elapsedTime < x.Current.TimerDuration) {
                    await UniTask.Yield();
                    x._elapsedTime += Time.deltaTime;
                }
            } finally {
                x.TimerRunning = false;
            }
        }, this);
    }

    void OnSkipTimer() {
        TimerRunning = false;
        _elapsedTime = Current!.TimerDuration;
    }

    void OnNext() {
        if (Current == null) return;
        if (TimerAvailable && !TimerRunning && _elapsedTime < Current.TimerDuration) {
            OnStartTimer();
            return;
        }

        OnSkipTimer();
        if (_elapsedTime < Current.TimerDuration) return;
        Current.Title.StringChanged -= OnTitleChanged;
        Current.Description.StringChanged -= OnDescriptionChanged;
        Current.Next.StringChanged -= OnNextChanged;
        if (Current.NextReference.IsEmpty) {
            _navigationService.Clear();
            _navigationService.Push("Start");
            UniTask.Void(static async x => await x.LoadNext(x._initialPhaseReference, CancellationToken.None),
                this);
            return;
        }

        UniTask.Void(static async x => await x.LoadNext(x.Current!.NextReference, CancellationToken.None), this);
    }

    void OnTitleChanged(string value) {
        Title = value;
    }

    void OnDescriptionChanged(string value) {
        Descriptions = new List<string>();
        var lines = value.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var builder = new StringBuilder();
        builder.Append(lines[0].TrimStart('-').Trim());
        for (var i = 1; i < lines.Length; ++i) {
            if (lines[i] is ['-', ' ', .. var content]) {
                Descriptions.Add(builder.ToString());
                builder.Clear();
                builder.Append(content);
                continue;
            }

            builder.Append('\n');
            builder.Append(lines[i].Trim());
        }

        Descriptions.Add(builder.ToString());
    }

    void OnNextChanged(string value) {
        Next = value;
    }
}
