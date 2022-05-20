using KrimTweaks.Configuration;
using SiraUtil.Affinity;

namespace KrimTweaks.Affinity_Patches.VFX;

internal class SaberBurnSparkles : IAffinity
{
    private readonly PluginConfig _config;

    public SaberBurnSparkles(PluginConfig config)
    {
        _config = config;
    }

    [AffinityPostfix]
    [AffinityPatch(typeof(SaberBurnMarkSparkles), nameof(SaberBurnMarkSparkles.Start))]
    // ReSharper disable once InconsistentNaming
    internal void Postfix(SaberBurnMarkSparkles __instance)
    {
        __instance.gameObject.SetActive(!_config.VFX.DisableFloorBurnParticles);
    }
}