using System;
using KrimTweaks.Configuration;
using Polyglot;
using SiraUtil.Attributes;
using UnityEngine;
using Zenject;

namespace KrimTweaks.Managers;

[Bind]
internal class Localizer : IInitializable, IDisposable
{
    internal static LocalizationAsset? LocalizationAsset;

    private PluginConfig _config;
    
    public Localizer(PluginConfig config)
    {
        _config = config;
    }

    public void Initialize()
    {
        _config.PropertyChanged.AddListener(Load);
        
        Load();
    }
    
    public void Dispose()
    {
        _config.PropertyChanged.RemoveListener(Load);
    }

    public void Load()
    {
        if (LocalizationAsset != null)
            Localization.Instance.InputFiles.Remove(LocalizationAsset);
        
        SiraUtil.Extras.Utilities.AssemblyFromPath("KrimTweaks.Resources.locales.csv", out var assembly, out var path);
        var original = SiraUtil.Extras.Utilities.GetResourceContent(assembly, path).Split('\n');
        var content = original[0];
        var baseLocalization = original[1];

        for (var i = 0; i < _config?.Extras.ExtraColorSchemeCount; i++)
        {
            content += baseLocalization.Replace("{idx}", (i + 4).ToString()) + "\n";
        }

        LocalizationAsset = new LocalizationAsset()
        {
            Format = GoogleDriveDownloadFormat.CSV,
            TextAsset = new TextAsset(content)
        };
        
        Localization.Instance.InputFiles.Add(LocalizationAsset);
        LocalizationImporter.Refresh();
    }
}