using KrimTweaks.Configuration;
using SiraUtil.Affinity;

namespace KrimTweaks.Affinity_Patches.VFX;

internal class CutParticles : IAffinity
{
    private readonly PluginConfig _config;
    
    public CutParticles(PluginConfig config)
    {
        _config = config;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(NoteCutParticlesEffect), nameof(NoteCutParticlesEffect.SpawnParticles))]
    internal bool Prefix()
    {
        return !_config.VFX.DisableCutParticles;
    }
}