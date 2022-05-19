using System;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using Zenject;

namespace KrimTweaks.UI;

public class KrimTweaksMenuButton : IInitializable, IDisposable
{
    private readonly MenuButton _menuButton;
    private readonly MainFlowCoordinator _mainFlowCoordinator;
    private readonly KrimTweaksFlowCoordinator _flowCoordinator;

    public KrimTweaksMenuButton(MainFlowCoordinator mainFlowCoordinator, KrimTweaksFlowCoordinator flowCoordinator)
    {
        _mainFlowCoordinator = mainFlowCoordinator;
        _flowCoordinator = flowCoordinator;
        _menuButton = new MenuButton("KrimTweaks", () =>
        {
            _mainFlowCoordinator.PresentFlowCoordinator(_flowCoordinator);
        });
    }
    
    public void Initialize()
    {
        MenuButtons.instance.RegisterButton(_menuButton);
    }

    public void Dispose()
    {
        if (MenuButtons.IsSingletonAvailable && BSMLParser.IsSingletonAvailable)
            MenuButtons.instance.UnregisterButton(_menuButton);
    }
}