using KrimTweaks.Configuration;
using SiraUtil.Affinity;

namespace KrimTweaks.Affinity_Patches.Menu;

internal class PromoBanner : IAffinity
{
    private readonly PluginConfig _config;

    public PromoBanner(PluginConfig config)
    {
        _config = config;
    }
    
    [AffinityPostfix]
    [AffinityPatch(typeof(MainMenuViewController), "DidActivate")]
    // ReSharper disable once InconsistentNaming
    internal void Postfix(ref MusicPackPromoBanner ____musicPackPromoBanner)
    {
        ____musicPackPromoBanner.gameObject.SetActive(!_config.Menu.RemovePromoBanners);
    }

}