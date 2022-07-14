using System;
using BeatSaberMarkupLanguage.FloatingScreen;
using HMUI;
using KrimTweaks.Configuration;
using KrimTweaks.UI.Clock;
using UnityEngine;
using Zenject;

namespace KrimTweaks.Managers.Clock;

// ReSharper disable FieldCanBeMadeReadOnly.Local
internal class Clock : IInitializable, IDisposable, ITickable
{
    [Inject] private PluginConfig _config = null!;
    [Inject] private ClockViewController _viewController = null!;

    private FloatingScreen _floatingScreen = null!;
    private float _hue = 0f;
    
    public void Initialize()
    {
        _floatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(150f, 50f), false,
            _config.Clock.Position.Value, Quaternion.Euler(_config.Clock.Rotation.Value));
        _floatingScreen.SetRootViewController(_viewController, ViewController.AnimationType.Out);

        _config.PropertyChanged.AddListener(Update);
        Update();
    }

    public void Dispose()
    {
        _config.PropertyChanged.RemoveListener(Update);
    }

    private void Update()
    {
        _floatingScreen.ScreenPosition = _config.Clock.Position.Value;
        _floatingScreen.ScreenRotation = Quaternion.Euler(_config.Clock.Rotation.Value);

        var clockColor = _config.Clock.Color;
        clockColor.a = _config.Clock.Opacity;
        _viewController.ClockColor = "#" + ColorUtility.ToHtmlStringRGBA(clockColor);
    }

    public void Tick()
    {
        if (!_config.Clock.Enabled)
        {
            _viewController.ClockText = "";
            return;
        }

        if (_config.Clock.Rainbow)
        {
            _hue += 0.008333f;
            if (_hue > 1f)
                _hue -= 1f;

            var color = Color.HSVToRGB(_hue, 1f, 1f);
            color.a = _config.Clock.Opacity;
            _viewController.ClockColor = "#" + ColorUtility.ToHtmlStringRGBA(color);
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