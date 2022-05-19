using BeatSaberMarkupLanguage;
using SiraUtil.Logging;
using HMUI;
using System;
using Zenject;

namespace KrimTweaks.UI;

public class KrimTweaksFlowCoordinator : FlowCoordinator
{
    private SiraLog? _siraLog;
    private MainFlowCoordinator? _mainFlowCoordinator;
    private KrimTweaksViewController? _viewController;

    [Inject]
    public void Construct(SiraLog siraLog, MainFlowCoordinator mainFlowCoordinator, KrimTweaksViewController viewController)
    {
        _mainFlowCoordinator = mainFlowCoordinator;
        _siraLog = siraLog;
        _viewController = viewController;
    }

    protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
    {
        try
        {
            if (!firstActivation) return;
            SetTitle("KrimTweaks");
            showBackButton = true;
            ProvideInitialViewControllers(_viewController);
        }
        catch (Exception ex)
        {
            _siraLog?.Error(ex);
        }
    }

    // ReSharper disable once ParameterHidesMember
    protected override void BackButtonWasPressed(ViewController topViewController)
    {
        _mainFlowCoordinator.DismissFlowCoordinator(this);
    }
}