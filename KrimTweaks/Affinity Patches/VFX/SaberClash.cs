using KrimTweaks.Configuration;
using SiraUtil.Affinity;

namespace KrimTweaks.Affinity_Patches.VFX;

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
        __result = false;
        return false;
    }
}