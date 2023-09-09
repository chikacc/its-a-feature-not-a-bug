using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

namespace FeatureNotBug; 

public static class TimeSpanToMinSecStringConverter {
    static readonly Regex _regex = new(@"(?<minutes>\d+):(?<seconds>\d+)", RegexOptions.ExplicitCapture);

    [RuntimeInitializeOnLoadMethod]
    static void OnLoadMethod() {
        var group = new ConverterGroup("TimeSpanToMinSecString");
        group.AddConverter((ref TimeSpan v) => {
            var seconds = (int)Math.Round(v.TotalSeconds);
            var minutes = Math.DivRem(seconds, 60, out seconds);
            return $"<mspace=0.6em>{minutes:00}:{seconds:00}</mspace>";
        });
        group.AddConverter((ref string v) => {
            var match = _regex.Match(v);
            if (!match.Success) return TimeSpan.Zero;
            return TimeSpan.FromMinutes(int.Parse(match.Groups["minutes"].Value))
                + TimeSpan.FromSeconds(int.Parse(match.Groups["seconds"].Value));
        });
        ConverterGroups.RegisterConverterGroup(group);
    }
}
