using KrimTweaks.Behaviours.Clock;
using KrimTweaks.Behaviours.Gameplay;
using KrimTweaks.Behaviours.VFX;
using KrimTweaks.Configuration;
using KrimTweaks.UI.Clock;
using Zenject;

namespace KrimTweaks.Installers;

internal class GameInstaller : Installer
{
    public override void InstallBindings()
    {
        var config = Container.Resolve<PluginConfig>();
        
        if (config.Gameplay.RemoveMusicGroupLogos)
            Container.BindInterfacesAndSelfTo<MusicGroupLogoRemover>().FromNewComponentOnNewGameObject().AsSingle();

        if (config.Clock.ShowInSong && config.Clock.Enabled)
        {
            Container.Bind<ClockViewController>().FromNewComponentAsViewController().AsSingle();
            Container.BindInterfacesTo<Clock>().AsSingle();
        }

        Container.BindInterfacesAndSelfTo<WorldParticlesRemover>().FromNewComponentOnNewGameObject().AsSingle();
    }
}