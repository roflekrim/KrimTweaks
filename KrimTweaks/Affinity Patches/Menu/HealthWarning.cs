using IPA.Utilities;
using SiraUtil.Affinity;
using SiraUtil.Logging;
using UnityEngine.UI;
using Zenject;

namespace KrimTweaks.Affinity_Patches.Menu;

internal class HealthWarning : IAffinity
{
    private readonly SiraLog _siraLog;
    
    public HealthWarning(SiraLog siraLog)
    {
        _siraLog = siraLog;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(DefaultScenesTransitionsFromInit), nameof(DefaultScenesTransitionsFromInit.TransitionToNextScene))]
    // ReSharper disable once RedundantAssignment
    internal void Prefix(ref bool goStraightToMenu)
    {
        goStraightToMenu = true;

#if DEBUG
        _siraLog.Debug("Skipped health warning");
#endif
    }
    
}