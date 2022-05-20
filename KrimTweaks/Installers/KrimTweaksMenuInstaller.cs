using KrimTweaks.UI;
using Zenject;

namespace KrimTweaks.Installers;

internal class MenuInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<KrimTweaksViewController>().FromNewComponentAsViewController().AsSingle();
        Container.Bind<KrimTweaksFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesAndSelfTo<KrimTweaksMenuButton>().AsSingle();
    }
}