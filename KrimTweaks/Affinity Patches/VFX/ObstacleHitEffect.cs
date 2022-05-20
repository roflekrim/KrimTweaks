using KrimTweaks.Configuration;
using SiraUtil.Affinity;

namespace KrimTweaks.Affinity_Patches.VFX;

internal class ObstacleHitEffect : IAffinity
{
    private readonly PluginConfig _config;

    public ObstacleHitEffect(PluginConfig config)
    {
        _config = config;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(ObstacleSaberSparkleEffect), nameof(ObstacleSaberSparkleEffect.StartEmission))]
    // ReSharper disable once InconsistentNaming
    internal bool Prefix(ObstacleSaberSparkleEffect __instance)
    {
        return !_config.VFX.DisableObstacleParticles;
    }
}