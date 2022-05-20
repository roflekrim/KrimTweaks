using KrimTweaks.Configuration;
using SiraUtil.Affinity;

namespace KrimTweaks.Affinity_Patches.VFX;

internal class SaberBurnArea : IAffinity
{
    private readonly PluginConfig _config;

    public SaberBurnArea(PluginConfig config)
    {
        _config = config;
    }

    [AffinityPostfix]
    [AffinityPatch(typeof(SaberBurnMarkArea), nameof(SaberBurnMarkArea.Start))]
    // ReSharper disable once InconsistentNaming
    internal void Postfix(SaberBurnMarkArea __instance)
    {
        __instance.gameObject.SetActive(!_config.VFX.DisableFloorBurnMarks);
    }
}