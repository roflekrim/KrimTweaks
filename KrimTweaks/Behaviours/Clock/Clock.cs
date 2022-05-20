using System;
using BeatSaberMarkupLanguage.FloatingScreen;
using HMUI;
using KrimTweaks.Configuration;
using KrimTweaks.UI.Clock;
using UnityEngine;
using Zenject;

namespace KrimTweaks.Behaviours.Clock;

// ReSharper disable FieldCanBeMadeReadOnly.Local
internal class Clock : IInitializable, ITickable
{
    [Inject] private PluginConfig _config = null!;
    [Inject] private ClockViewController _viewController = null!;

    private FloatingScreen _floatingScreen = null!;
    
    public void Initialize()
    {
        _floatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(150f, 50f), false,
            new Vector3(0.0f, 3f, 3.9f), Quaternion.Euler(325f, 0.0f, 0.0f));
        _floatingScreen.SetRootViewController(_viewController, ViewController.AnimationType.Out);
    }

    public void Tick()
    {
        if (!_config.Clock.Enabled)
        {
            _viewController.ClockText = "";
            return;
        }
        var now = DateTime.Now;
        var s = now.ToString(_config.Clock.SelectedTimeFormat);
        
        if (_config.Clock.DisplaySessionLength)
        {
            var timeSpan = now.Subtract(Plugin.Started);
            s += $"\n{Math.Floor(timeSpan.TotalHours)}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
        }

        _viewController.ClockText = s;
    }
}