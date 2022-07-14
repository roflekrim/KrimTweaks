using KrimTweaks.Configuration;
using SiraUtil.Affinity;
using SiraUtil.Attributes;

namespace KrimTweaks.Affinity_Patches.VFX;

[Bind]
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