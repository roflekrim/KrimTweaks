using KrimTweaks.Behaviours.Gameplay;
using KrimTweaks.Configuration;
using Zenject;

namespace KrimTweaks.Installers;

internal class GameInstaller : Installer
{
    public override void InstallBindings()
    {
        var config = Container.Resolve<PluginConfig>();
        
        if (config.Gameplay.RemoveMusicGroupLogos)
            Container.BindInterfacesAndSelfTo<MusicGroupLogoRemover>().FromNewComponentOnNewGameObject().AsSingle();
    }
}