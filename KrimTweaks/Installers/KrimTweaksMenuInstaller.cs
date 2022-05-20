using KrimTweaks.Behaviours.Clock;
using KrimTweaks.Behaviours.Menu;
using KrimTweaks.Behaviours.VFX;
using KrimTweaks.UI;
using KrimTweaks.UI.Clock;
using KrimTweaks.UI.Settings;
using Zenject;

namespace KrimTweaks.Installers;

internal class MenuInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<KrimTweaksViewController>().FromNewComponentAsViewController().AsSingle();
        Container.Bind<KrimTweaksFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesAndSelfTo<KrimTweaksMenuButton>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<MenuNotes>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesAndSelfTo<WorldParticlesRemover>().FromNewComponentOnNewGameObject().AsSingle();

        Container.Bind<ClockViewController>().FromNewComponentAsViewController().AsSingle();
        Container.BindInterfacesTo<Clock>().AsSingle();
    }
}