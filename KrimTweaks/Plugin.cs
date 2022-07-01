using System;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using KrimTweaks.Configuration;
using KrimTweaks.Installers;
using IPALogger = IPA.Logging.Logger;

namespace KrimTweaks;

[Plugin(RuntimeOptions.DynamicInit), NoEnableDisable]
public class Plugin
{
    internal const string SCORESABER_STATUS_TEXT =
        "Wrapper/MenuCore/UI/ScreenSystem/ScreenContainer/RightScreen/PlatformLeaderboardViewController/ScoreSaberPanelScreen/PanelView/BSMLBackground/BSMLHorizontalLayoutGroup/BSMLText";
    
    internal static DateTime Started;
    
    [Init]
    public void Init(Zenjector zenjector, IPALogger logger, Config config)
    {
        var conf = config.Generated<PluginConfig>();
        if (!conf.Enabled)
            return;
        
        Started = DateTime.Now;

        zenjector.UseLogger(logger);
        zenjector.UseMetadataBinder<Plugin>();
        
        zenjector.Install<AppInstaller>(Location.App, conf);
        zenjector.Install<MenuInstaller>(Location.Menu);
        zenjector.Install<GameInstaller>(Location.GameCore);
    }
}