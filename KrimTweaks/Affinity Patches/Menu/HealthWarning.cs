using KrimTweaks.Configuration;
using SiraUtil.Affinity;
using SiraUtil.Attributes;
using SiraUtil.Logging;

namespace KrimTweaks.Affinity_Patches.Menu;

[Bind]
internal class HealthWarning : IAffinity
{
    private readonly SiraLog _siraLog;
    private readonly PluginConfig _config;
    
    public HealthWarning(SiraLog siraLog, PluginConfig config)
    {
        _siraLog = siraLog;
        _config = config;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(DefaultScenesTransitionsFromInit), nameof(DefaultScenesTransitionsFromInit.TransitionToNextScene))]
    // ReSharper disable once RedundantAssignment
    internal void Prefix(ref bool goStraightToMenu)
    {
        if (_config.Menu.SkipHealthWarning)
            goStraightToMenu = true;
    }
}