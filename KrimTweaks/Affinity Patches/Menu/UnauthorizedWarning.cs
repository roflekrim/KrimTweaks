using System.Linq;
using BeatSaberMarkupLanguage.Components;
using HMUI;
using IPA.Utilities;
using KrimTweaks.Configuration;
using KrimTweaks.UI.LevelSelectionWarning;
using SiraUtil.Affinity;
using SiraUtil.Attributes;
using SiraUtil.Logging;
using SiraUtil.Zenject;
using UnityEngine;

namespace KrimTweaks.Affinity_Patches.Menu;

[Bind(Location.Menu)]
internal class UnauthorizedWarning : IAffinity
{
    private static readonly string[] UnauthorizedMessages = new[]
    {
        "Failed, attempting again",
        "Failed to authenticate with ScoreSaber! Please restart your game"
    };

    private readonly SiraLog _siraLog;
    private readonly PluginConfig _config;
    private readonly LevelSelectionWarningViewController _view;

    public UnauthorizedWarning(SiraLog siraLog, PluginConfig config, LevelSelectionWarningViewController view)
    {
        _siraLog = siraLog;
        _config = config;
        _view = view;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(SinglePlayerLevelSelectionFlowCoordinator), "ActionButtonWasPressed")]
    // ReSharper disable once InconsistentNaming
    internal bool Prefix(SinglePlayerLevelSelectionFlowCoordinator __instance)
    {
        if (!_config.Extras.ShowWarningOnPlay) return true;
        if (GameObject.Find(Plugin.SCORESABER_STATUS_TEXT) is { } go)
        {
            var text = go.GetComponent<FormattableText>().text;
            if (!UnauthorizedMessages.Any(s => text.Contains(s)))
                return true;
        }
        
        _view.FlowCoordinator = __instance; 
        _view.Text = "<size=10>Unauthorized</size>\n\n<size=6>ScoreSaber authentication failed, score submission will not be possible!";
        __instance.InvokeMethod<object, SinglePlayerLevelSelectionFlowCoordinator>("PresentViewController", 
            _view, null, ViewController.AnimationDirection.Horizontal, false);
        return false;
    }
}