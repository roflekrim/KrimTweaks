using KrimTweaks.Configuration;
using SiraUtil.Affinity;
using SiraUtil.Attributes;

namespace KrimTweaks.Affinity_Patches.VFX;

[Bind]
internal class SaberClash : IAffinity
{
    private readonly PluginConfig _config;

    public SaberClash(PluginConfig config)
    {
        _config = config;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(SaberClashChecker), nameof(SaberClashChecker.AreSabersClashing))]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once RedundantAssignment
    internal bool Prefix(ref bool __result)
    {
        if (!_config.VFX.DisableSaberClash) return true;
        __result = false;
        return false;
    }
}