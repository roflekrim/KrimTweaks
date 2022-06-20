using KrimTweaks.Configuration;
using SiraUtil.Affinity;

namespace KrimTweaks.Affinity_Patches.VFX;

internal class FullComboBreak : IAffinity
{
    private readonly PluginConfig _config;

    public FullComboBreak(PluginConfig config)
    {
        _config = config;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(ComboUIController), nameof(ComboUIController.HandleComboBreakingEventHappened))]
    // ReSharper disable InconsistentNaming
    internal bool Prefix(ComboUIController __instance, ref bool ____fullComboLost)
    {
        if (!_config.VFX.DisableFullComboBreak)
            return true;

        if (____fullComboLost) return true;
        ____fullComboLost = true;
        __instance.transform.Find("Line0")?.gameObject.SetActive(false);
        __instance.transform.Find("Line1")?.gameObject.SetActive(false);
        return false;

    }
}