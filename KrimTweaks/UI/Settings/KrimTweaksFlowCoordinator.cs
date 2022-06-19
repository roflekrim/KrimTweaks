using System;
using BeatSaberMarkupLanguage;
using HMUI;
using SiraUtil.Logging;
using Zenject;

namespace KrimTweaks.UI.Settings;

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
        _viewController._flowCoordinator = this;
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

    internal void SetLeftViewController(ViewController? viewController)
    {
        if (viewController is null)
            SetLeftScreenViewController(null, ViewController.AnimationType.Out);
        else
            SetLeftScreenViewController(viewController, ViewController.AnimationType.In);
    }
    
    internal void SetRightViewController(ViewController? viewController)
    {
        if (viewController is null)
            SetRightScreenViewController(null, ViewController.AnimationType.Out);
        else
            SetRightScreenViewController(viewController, ViewController.AnimationType.In);
    }
}