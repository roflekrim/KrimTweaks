using KrimTweaks.Configuration;
using SiraUtil.Affinity;
using SiraUtil.Attributes;

namespace KrimTweaks.Affinity_Patches.Gameplay;

[Bind]
internal class DisableBeatLines : IAffinity
{

    private readonly PluginConfig _config;

    public DisableBeatLines(PluginConfig config)
    {
        _config = config;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(BeatLineManager), nameof(BeatLineManager.Start))]
    // ReSharper disable once InconsistentNaming
    internal bool Prefix(BeatLineManager __instance)
    {
        if (!_config.Gameplay.DisableBeatLines)
            return true;
        
        __instance.enabled = false;
        return false;
    }

}