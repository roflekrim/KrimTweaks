using System.Collections.Generic;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using UnityEngine.Events;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]

namespace KrimTweaks.Configuration;

#region Sub Configurations

public class MenuConfig
{
    public virtual bool UseStaticLightsToggle { get; set; } = false;
    public virtual bool RemovePromoBanners { get; set; } = false;
    public virtual bool SkipHealthWarning { get; set; } = false;
    public virtual bool RemoveMenuNotes { get; set; } = false;
}

public class GameplayConfig
{
    public virtual bool DisableDebris { get; set; } = false;
    public virtual bool RemoveMusicGroupLogos { get; set; } = false;
    public virtual bool DisableBeatLines { get; set; } = false;
    public virtual bool DisableRumble { get; set; } = false;
}

public class VFXConfig
{
    public virtual bool DisableWorldParticles { get; set; } = false;
    public virtual bool DisableCutParticles { get; set; } = false;
    public virtual bool DisableBombParticles { get; set; } = false;
    public virtual bool DisableSaberClash { get; set; } = false;
    public virtual bool DisableObstacleParticles { get; set; } = false;
    public virtual bool DisableFloorBurnParticles { get; set; } = false;
    public virtual bool DisableFloorBurnMarks { get; set; } = false;
    public virtual bool DisableFullComboBreak { get; set; } = false;
    public virtual bool DisableCameraNoiseDither { get; set; } = false;
}

public class ClockConfig
{
    public virtual bool Enabled { get; set; } = false;
    public virtual bool ShowInSong { get; set; } = false;
    public virtual bool DisplaySessionLength { get; set; } = false;
    public virtual string SelectedTimeFormat { get; set; } = "HH:mm:ss";

    [NonNullable]
    [UseConverter(typeof(ListConverter<string>))]
    public virtual List<string> TimeFormats { get; set; } = new()
    {
        "h:mm tt",
        "h:mm:ss tt",
        "HH:mm",
        "HH:mm:ss",
    };
}

#endregion

public class PluginConfig
{
    public virtual bool Enabled { get; set; } = true;

    public virtual MenuConfig Menu { get; set; } = new();
    public virtual GameplayConfig Gameplay { get; set; } = new();
    public virtual VFXConfig VFX { get; set; } = new();
    public virtual ClockConfig Clock { get; set; } = new();

    public virtual void Changed() => PropertyChanged.Invoke();
    
    [Ignore]
    public readonly UnityEvent PropertyChanged = new();
}