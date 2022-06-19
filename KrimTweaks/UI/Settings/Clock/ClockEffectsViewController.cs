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

    private PluginConfig _config = null!;

    [Inject]
    public void Construct(PluginConfig config)
    {
        _config = config;
    }

    [UIValue("color")]
    protected Color Color
    {
        get => _config.Clock.Color;
        set
        {
            value.a = _config.Clock.Opacity;
            _config.Clock.Color = value;
        }
    }

    [UIValue("opacity")]
    protected float Opacity
    {
        get => _config.Clock.Opacity;
        set
        {
            var clockColor = _config.Clock.Color;
            clockColor.a = value;
            _config.Clock.Color = clockColor;
            _config.Clock.Opacity = value;
        }
    }

    [UIValue("rainbow")]
    protected bool Rainbow
    {
        get => _config.Clock.Rainbow;
        set => _config.Clock.Rainbow = value;
    }

    [UIAction("f-percent")]
    protected string PercentFormatter(float value)
    {
        return $"{Math.Round(value * 100)}%";
    }

}