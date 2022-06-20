using System;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using KrimTweaks.Configuration;
using UnityEngine;
using Zenject;

namespace KrimTweaks.UI.Settings.Clock;

[ViewDefinition("KrimTweaks.UI.BSML.Clock.ClockEffectsView.bsml")]
[HotReload(RelativePathToLayout = @"..\..\BSML\Clock\ClockEffectsView.bsml")]
public class ClockEffectsViewController : BSMLAutomaticViewController
{
    private ClockConfig _config = null!;

    [Inject]
    public void Construct(PluginConfig config)
    {
        _config = config.Clock;
    }

    [UIAction("f-percent")]
    protected string PercentFormatter(float value)
    {
        return $"{Math.Round(value * 100)}%";
    }
}