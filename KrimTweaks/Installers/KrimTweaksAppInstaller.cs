using KrimTweaks.Affinity_Patches.Extras;
using KrimTweaks.Affinity_Patches.Gameplay;
using KrimTweaks.Affinity_Patches.Menu;
using KrimTweaks.Affinity_Patches.VFX;
using KrimTweaks.Configuration;
using KrimTweaks.Managers;
using Zenject;

namespace KrimTweaks.Installers;

internal class AppInstaller : Installer
{
    private readonly PluginConfig _config;

    public AppInstaller(PluginConfig config)
    {
        _config = config;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(_config);
    }
}