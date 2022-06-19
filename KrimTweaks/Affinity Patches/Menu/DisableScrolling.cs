using System.Numerics;
using HMUI;
using KrimTweaks.Configuration;
using SiraUtil.Affinity;

namespace KrimTweaks.Affinity_Patches.Menu;

internal class DisableScrolling : IAffinity
{
    private readonly PluginConfig _config;
    
    public DisableScrolling(PluginConfig config)
    {
        _config = config;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(HMUI.ScrollView), nameof(ScrollView.HandleJoystickWasNotCenteredThisFrame))]
    // ReSharper disable once RedundantAssignment
    internal void Prefix(ref Vector2 deltaPos)
    {
        deltaPos *= _config.Extras.DisableScrolling ? Vector2.Zero : Vector2.One;
    }
}