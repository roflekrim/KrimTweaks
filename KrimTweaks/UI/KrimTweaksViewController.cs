using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.ViewControllers;
using KrimTweaks.Configuration;
using Zenject;

namespace KrimTweaks.UI;

[ViewDefinition("KrimTweaks.UI.BSML.KrimTweaksView.bsml")]
[HotReload(RelativePathToLayout = @".\BSML\KrimTweaksView.bsml")]
public class KrimTweaksViewController : BSMLAutomaticViewController
{
    private PluginConfig _config = null!;
    
    [Inject]
    public void Construct(PluginConfig config)
    {
        _config = config;
    }

    #region Menu

    [UIValue("menu-static-lights")]
    protected bool MenuStaticLights
    {
        get => _config.Menu.UseStaticLightsToggle;
        set => _config.Menu.UseStaticLightsToggle = value;
    }
    
    [UIValue("menu-promo-banners")]
    protected bool MenuPromoBanners
    {
        get => _config.Menu.RemovePromoBanners;
        set => _config.Menu.RemovePromoBanners = value;
    }
    
    [UIValue("menu-skip-health")]
    protected bool MenuSkipHealthWarning
    {
        get => _config.Menu.SkipHealthWarning;
        set => _config.Menu.SkipHealthWarning = value;
    }
    
    [UIValue("menu-remove-notes")]
    protected bool MenuRemoveNotes
    {
        get => _config.Menu.RemoveMenuNotes;
        set => _config.Menu.RemoveMenuNotes = value;
    }

    #endregion

    #region Gameplay

    [UIValue("gameplay-debris")]
    protected bool GameplayDisableDebris
    {
        get => _config.Gameplay.DisableDebris;
        set => _config.Gameplay.DisableDebris = value;
    }

    [UIValue("gameplay-group-logos")]
    protected bool GameplayRemoveMusicGroupLogos
    {
        get => _config.Gameplay.RemoveMusicGroupLogos;
        set => _config.Gameplay.RemoveMusicGroupLogos = value;
    }

    [UIValue("gameplay-beat-lines")]
    protected bool GameplayDisableBeatLines
    {
        get => _config.Gameplay.DisableBeatLines;
        set => _config.Gameplay.DisableBeatLines = value;
    }

    [UIValue("gameplay-rumble")]
    protected bool GameplayDisableRumble
    {
        get => _config.Gameplay.DisableRumble;
        set => _config.Gameplay.DisableRumble = value;
    }

    #endregion
    
    #region VFX

    [UIValue("vfx-world")]
    protected bool VFXWorld
    {
        get => _config.VFX.DisableWorldParticles;
        set => _config.VFX.DisableWorldParticles = value;
    }
    
    [UIValue("vfx-cut")]
    protected bool VFXCut
    {
        get => _config.VFX.DisableCutParticles;
        set => _config.VFX.DisableCutParticles = value;
    }
    
    [UIValue("vfx-bomb")]
    protected bool VFXBomb
    {
        get => _config.VFX.DisableBombParticles;
        set => _config.VFX.DisableBombParticles = value;
    }
    
    [UIValue("vfx-saber-clash")]
    protected bool VFXSaberClash
    {
        get => _config.VFX.DisableSaberClash;
        set => _config.VFX.DisableSaberClash = value;
    }
    
    [UIValue("vfx-obstacle")]
    protected bool VFXObstacle
    {
        get => _config.VFX.DisableObstacleParticles;
        set => _config.VFX.DisableObstacleParticles = value;
    }
    
    [UIValue("vfx-floor-particles")]
    protected bool VFXFloorBurnParticles
    {
        get => _config.VFX.DisableFloorBurnParticles;
        set => _config.VFX.DisableFloorBurnParticles = value;
    }
    
    [UIValue("vfx-floor-marks")]
    protected bool VFXFloorBurnMarks
    {
        get => _config.VFX.DisableFloorBurnMarks;
        set => _config.VFX.DisableFloorBurnMarks = value;
    }
    
    [UIValue("vfx-fc-break")]
    protected bool VFXFullComboBreak
    {
        get => _config.VFX.DisableFullComboBreak;
        set => _config.VFX.DisableFullComboBreak = value;
    }
    
    [UIValue("vfx-cam-noise-dither")]
    protected bool VFXDisableCameraNoiseDither
    {
        get => _config.VFX.DisableCameraNoiseDither;
        set => _config.VFX.DisableCameraNoiseDither = value;
    }

    #endregion
    
    #region Audio

    [UIValue("audio-enable-normalization")]
    protected bool AudioEnableNormalization
    {
        get => _config.Audio.EnableNormalization;
        set => _config.Audio.EnableNormalization = value;
    }
    
    [UIValue("audio-min-loudness")]
    protected float AudioMinLoudness
    {
        get => _config.Audio.MinLoudness;
        set => _config.Audio.MinLoudness = value;
    }
    
    [UIValue("audio-max-loudness")]
    protected float AudioMaxLoudness
    {
        get => _config.Audio.MaxLoudness;
        set => _config.Audio.MaxLoudness = value;
    }

    [UIAction("audio-dB")]
    public string Formatter_dB(float val)
    {
        return $"{val} dB";
    }

    #endregion

    #region Clock

    [UIValue("clock-enable")]
    protected bool ClockEnabled
    {
        get => _config.Clock.Enabled;
        set => _config.Clock.Enabled = value;
    }

    [UIValue("clock-in-song")]
    protected bool ClockShowInSong
    {
        get => _config.Clock.ShowInSong;
        set => _config.Clock.ShowInSong = value;
    }

    [UIValue("clock-session-length")]
    protected bool ClockShowSessionLength
    {
        get => _config.Clock.DisplaySessionLength;
        set => _config.Clock.DisplaySessionLength = value;
    }

    [UIValue("clock-format")]
    protected string ClockTimeFormat
    {
        get => _config.Clock.SelectedTimeFormat;
        set => _config.Clock.SelectedTimeFormat = value;
    }

    [UIValue("clock-formats")]
    protected List<object> ClockTimeFormats => _config.Clock.TimeFormats.Select(s => s as object).ToList();

    [UIAction("clock-formatter")]
    public string ClockFormatter(string format)
    {
        return DateTime.Now.ToString(format);
    }
    
    #endregion
}