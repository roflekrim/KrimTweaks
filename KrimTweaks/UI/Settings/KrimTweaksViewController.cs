using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using KrimTweaks.Configuration;
using KrimTweaks.UI.Settings.Clock;
using SiraUtil.Logging;
using Zenject;

namespace KrimTweaks.UI.Settings;

[ViewDefinition("KrimTweaks.UI.BSML.KrimTweaksView.bsml")]
[HotReload(RelativePathToLayout = @"..\BSML\KrimTweaksView.bsml")]
public class KrimTweaksViewController : BSMLAutomaticViewController
{
    private PluginConfig _config = null!;
    private SiraLog _siraLog = null!;
    private ClockEffectsViewController _effectsViewController = null!;
    private ClockTransformViewController _transformViewController = null!;
    
    internal KrimTweaksFlowCoordinator _flowCoordinator = null!;
    
    [Inject]
    public void Construct(PluginConfig config,
        SiraLog siraLog,
        ClockEffectsViewController effectsViewController,
        ClockTransformViewController transformViewController)
    {
        _config = config;
        _siraLog = siraLog;
        _effectsViewController = effectsViewController;
        _transformViewController = transformViewController;
    }

    [UIComponent("tabSelector")]
    private TabSelector? _tabSelector = null;

    [UIAction("tab-switch")]
    public void TabSwitch(SegmentedControl control, int index)
    {
        if (index == 3)
        {
            _flowCoordinator.SetLeftViewController(_effectsViewController);
            _flowCoordinator.SetRightViewController(_transformViewController);
        }
        else
        {
            _flowCoordinator.SetLeftViewController(null);
            _flowCoordinator.SetRightViewController(null);
        }
    }

    protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
    {
        base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);
        
        if (_tabSelector == null) return;
        _tabSelector.textSegmentedControl.SelectCellWithNumber(0);
        _tabSelector.Refresh();
        _tabSelector.Setup();
    }
    
}