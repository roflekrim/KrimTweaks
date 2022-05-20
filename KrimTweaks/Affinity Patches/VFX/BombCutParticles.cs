using KrimTweaks.Configuration;
using SiraUtil.Affinity;

namespace KrimTweaks.Affinity_Patches.VFX;

internal class BombCutParticles : IAffinity
{
    private readonly PluginConfig _config;

    public BombCutParticles(PluginConfig config)
    {
        _config = config;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(BombExplosionEffect), nameof(BombExplosionEffect.SpawnExplosion))]
    internal bool Prefix()
    {
        return !_config.VFX.DisableBombParticles;
    }
}