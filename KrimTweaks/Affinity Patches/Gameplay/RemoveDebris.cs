using KrimTweaks.Configuration;
using SiraUtil.Affinity;
using SiraUtil.Attributes;
using SiraUtil.Logging;

namespace KrimTweaks.Affinity_Patches.Gameplay;

[Bind]
internal class RemoveDebris : IAffinity
{
    private readonly SiraLog _siraLog;
    private readonly PluginConfig _config;

    public RemoveDebris(SiraLog siraLog, PluginConfig config)
    {
        _siraLog = siraLog;
        _config = config;
    }
    
    [AffinityPrefix]
    [AffinityPatch(typeof(NoteDebrisSpawner), nameof(NoteDebrisSpawner.SpawnDebris))]
    internal bool Prefix()
    {
        return !_config.Gameplay.DisableDebris;
    }
}