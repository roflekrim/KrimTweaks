using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using IPA.Utilities;
using UnityEngine;

namespace KrimTweaks.UI.LevelSelectionWarning;

[ViewDefinition("KrimTweaks.UI.BSML.WarningView.bsml")]
[HotReload(RelativePathToLayout = @"..\BSML\ClockView.bsml")]
internal class LevelSelectionWarningViewController : BSMLAutomaticViewController
{
    internal SinglePlayerLevelSelectionFlowCoordinator? FlowCoordinator;
    
    [UIValue("warning-text")]
    public string Text = "";

    [UIAction("cancel")]
    public void Cancel()
    {
        if (FlowCoordinator == null) return;
        FlowCoordinator.InvokeMethod<object, SinglePlayerLevelSelectionFlowCoordinator>("DismissViewController", 
            this, AnimationDirection.Horizontal, null, false);
    }
}