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
    private ClockConfig _config = null!;
    
    [Inject]
    public void Construct(PluginConfig config)
    {
        _config = config.Clock;
    }
}