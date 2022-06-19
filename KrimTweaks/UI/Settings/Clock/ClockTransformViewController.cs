using System;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using KrimTweaks.Configuration;
using Zenject;

namespace KrimTweaks.UI.Settings.Clock;

[ViewDefinition("KrimTweaks.UI.BSML.Clock.ClockTransformView.bsml")]
[HotReload(RelativePathToLayout = @"..\..\BSML\Clock\ClockTransformView.bsml")]
public class ClockTransformViewController : BSMLAutomaticViewController
{
    private PluginConfig _config = null!;
    
    [Inject]
    public void Construct(PluginConfig config)
    {
        _config = config;
    }

    [UIValue("pos-x")]
    protected float PositionX
    {
        get => _config.Clock.Position.x;
        set
        {
            // C# complained smh
            var foo = _config.Clock.Position;
            foo.x = value;
            _config.Clock.Position = foo;
        }
    }
    
    [UIValue("pos-y")]
    protected float PositionY
    {
        get => _config.Clock.Position.y;
        set
        {
            // C# complained smh
            var foo = _config.Clock.Position;
            foo.y = value;
            _config.Clock.Position = foo;
        }
    }
    
    [UIValue("pos-z")]
    protected float PositionZ
    {
        get => _config.Clock.Position.z;
        set
        {
            // C# complained smh
            var foo = _config.Clock.Position;
            foo.z = value;
            _config.Clock.Position = foo;
        }
    }
    
    [UIValue("rot-x")]
    protected float RotationX
    {
        get => _config.Clock.Rotation.x;
        set
        {
            // C# complained smh
            var foo = _config.Clock.Rotation;
            foo.x = value;
            _config.Clock.Rotation = foo;
        }
    }
    
    [UIValue("rot-y")]
    protected float RotationY
    {
        get => _config.Clock.Rotation.y;
        set
        {
            // C# complained smh
            var foo = _config.Clock.Rotation;
            foo.y = value;
            _config.Clock.Rotation = foo;
        }
    }
    
    [UIValue("rot-z")]
    protected float RotationZ
    {
        get => _config.Clock.Rotation.z;
        set
        {
            // C# complained smh
            var foo = _config.Clock.Rotation;
            foo.z = value;
            _config.Clock.Rotation = foo;
        }
    }

    [UIAction("f-deg")]
    public string DegreeFormatter(float f)
    {
        return $"{Math.Round(f)} deg";
    }
    
}