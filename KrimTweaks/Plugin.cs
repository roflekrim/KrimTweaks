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
    internal static DateTime Started;
    
    [Init]
    public void Init(Zenjector zenjector, IPALogger logger, Config config)
    {
        Started = DateTime.Now;

        zenjector.UseLogger(logger);
        zenjector.UseMetadataBinder<Plugin>();
        
        zenjector.Install<AppInstaller>(Location.App, config.Generated<PluginConfig>());
        zenjector.Install<MenuInstaller>(Location.Menu);
        zenjector.Install<GameInstaller>(Location.GameCore);
    }
}