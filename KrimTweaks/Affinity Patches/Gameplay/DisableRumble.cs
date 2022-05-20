using KrimTweaks.Configuration;
using SiraUtil.Affinity;
using SiraUtil.Logging;

namespace KrimTweaks.Affinity_Patches.Gameplay;

internal class DisableRumble : IAffinity
{
    private readonly SiraLog _siraLog;
    private readonly PluginConfig _config;
    
    public DisableRumble(SiraLog siraLog, PluginConfig config)
    {
        _siraLog = siraLog;
        _config = config;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(HapticFeedbackController), nameof(HapticFeedbackController.PlayHapticFeedback))]
    internal bool Prefix()
    {
        return !_config.Gameplay.DisableRumble;
    }
    
}