using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using UnityEngine;
using UnityEngine.Events;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]

namespace KrimTweaks.Configuration;

#region Models

public class XVector3
{
    internal Vector3 Value;

    public virtual float X
    {
        get => Value.x;
        set => Value.x = value;
    }
    
    public virtual float Y
    {
        get => Value.y;
        set => Value.y = value;
    }
    
    public virtual float Z
    {
        get => Value.z;
        set => Value.z = value;
    }

    public XVector3() => Value = new Vector3();
    
    public XVector3(Vector3 value)
    {
        Value = value;
    }

    public XVector3(float x, float y, float z)
    {
        Value = new Vector3(x, y, z);
    }
    
    public string DegreeFormatter(float f)
    {
        return $"{Math.Round(f)} deg";
    }
}

#endregion

#region Sub Configurations

public class MenuConfig
{
    public virtual bool UseStaticLightsToggle { get; set; } = false;
    public virtual bool RemovePromoBanners { get; set; } = false;
    public virtual bool SkipHealthWarning { get; set; } = false;
    public virtual bool RemoveMenuNotes { get; set; } = false;
    public virtual bool DisableAnniversary { get; set; } = false;
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
    public virtual XVector3 Position { get; set; } = new(0.0f, 3f, 3.9f);
    public virtual XVector3 Rotation { get; set; } = new(325f, 0.0f, 0.0f);
    public virtual bool Rainbow { get; set; } = false;
    public virtual Color Color { get; set; } = new(1, 1, 1, 1);
    public virtual float Opacity { get; set; } = 1.0f;
    
    [NonNullable]
    [UseConverter(typeof(ListConverter<string>))]
    public virtual List<string> TimeFormats { get; set; } = new()
    {
        "h:mm tt",
        "h:mm:ss tt",
        "HH:mm",
        "HH:mm:ss",
    };

    public List<object> BsmlTimeFormats => TimeFormats.Select(s => s as object).ToList();
    
    public string DateFormatter(string format)
    {
        return DateTime.Now.ToString(format);
    }
    
    public string PercentFormatter(float value)
    {
        return value.ToString("0%");
    }
}

public class ExtrasConfig
{
    public virtual bool DisableScrolling { get; set; } = false;
    public virtual bool ShowWarningOnPlay { get; set; } = false;
}

#endregion

public class PluginConfig
{
    public virtual bool Enabled { get; set; } = true;

    public virtual MenuConfig Menu { get; set; } = new();
    public virtual GameplayConfig Gameplay { get; set; } = new();
    public virtual VFXConfig VFX { get; set; } = new();
    public virtual ClockConfig Clock { get; set; } = new();
    public virtual ExtrasConfig Extras { get; set; } = new();

    public virtual void Changed() => PropertyChanged.Invoke();
    
    [Ignore]
    public readonly UnityEvent PropertyChanged = new();
}