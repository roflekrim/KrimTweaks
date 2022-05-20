using KrimTweaks.Affinity_Patches;
using KrimTweaks.Affinity_Patches.Gameplay;
using KrimTweaks.Affinity_Patches.Menu;
using KrimTweaks.Affinity_Patches.VFX;
using KrimTweaks.Configuration;
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
        
        if (_config.Menu.SkipHealthWarning)
            Container.BindInterfacesTo<HealthWarning>().AsSingle();

        Container.BindInterfacesTo<StaticLightsToggle>().AsSingle();
        Container.BindInterfacesTo<PromoBanner>().AsSingle();

        Container.BindInterfacesTo<RemoveDebris>().AsSingle();
        Container.BindInterfacesTo<DisableBeatLines>().AsSingle();
        Container.BindInterfacesTo<DisableRumble>().AsSingle();

        Container.BindInterfacesTo<CameraNoise>().AsSingle();
        Container.BindInterfacesTo<CutParticles>().AsSingle();
        Container.BindInterfacesTo<BombCutParticles>().AsSingle();
        Container.BindInterfacesTo<SaberClash>().AsSingle();
        Container.BindInterfacesTo<ObstacleHitEffect>().AsSingle();
        Container.BindInterfacesTo<SaberBurnSparkles>().AsSingle();
        Container.BindInterfacesTo<SaberBurnArea>().AsSingle();
        Container.BindInterfacesTo<FullComboBreak>().AsSingle();
    }
}