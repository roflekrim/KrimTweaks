using KrimTweaks.Configuration;
using SiraUtil.Affinity;
using UnityEngine;

namespace KrimTweaks.Affinity_Patches.VFX;

// 100% not yoinked from Tweaks55
internal class CameraNoise : IAffinity
{
    private static readonly int GlobalNoiseTextureID = Shader.PropertyToID("_GlobalBlueNoiseTex");
    
    private readonly PluginConfig _config;
    private bool _lastDisableState = false;

    public CameraNoise(PluginConfig config)
    {
        _config = config;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(BlueNoiseDitheringUpdater), nameof(BlueNoiseDitheringUpdater.HandleCameraPreRender))]
    internal bool Prefix()
    {
        if (!_config.VFX.DisableCameraNoiseDither)
        {
            _lastDisableState = false;
            return true;
        }

        if (_lastDisableState) return false;
        Shader.SetGlobalTexture(GlobalNoiseTextureID, null);
        _lastDisableState = true;
        return false;
    }

}