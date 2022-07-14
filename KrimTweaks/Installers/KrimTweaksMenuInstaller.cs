using KrimTweaks.Affinity_Patches.Menu;
using KrimTweaks.Managers.Clock;
using KrimTweaks.Managers.Menu;
using KrimTweaks.Managers.VFX;
using KrimTweaks.UI.Clock;
using KrimTweaks.UI.LevelSelectionWarning;
using KrimTweaks.UI.Settings;
using KrimTweaks.UI.Settings.Clock;
using Zenject;

namespace KrimTweaks.Installers;

internal class MenuInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<ClockEffectsViewController>().FromNewComponentAsViewController().AsSingle();
        Container.Bind<ClockTransformViewController>().FromNewComponentAsViewController().AsSingle();
        Container.Bind<KrimTweaksViewController>().FromNewComponentAsViewController().AsSingle();
        Container.Bind<KrimTweaksFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesAndSelfTo<KrimTweaksMenuButton>().AsSingle();

        Container.Bind<LevelSelectionWarningViewController>().FromNewComponentAsViewController().AsSingle();

        Container.Bind<ClockViewController>().FromNewComponentAsViewController().AsSingle();
        Container.BindInterfacesTo<Clock>().AsSingle();
    }
}