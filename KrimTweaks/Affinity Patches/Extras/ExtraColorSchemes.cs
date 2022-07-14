/*
Copyright (c) 2021 Meivyn
Modifications Copyright (c) 2022 roflekrim
2022-07-01: Update for 1.23.0, zenjectify & optimization

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using KrimTweaks.Configuration;
using SiraUtil.Affinity;
using SiraUtil.Attributes;
using Zenject;

namespace KrimTweaks.Affinity_Patches.Extras;

// ReSharper disable InconsistentNaming
[Bind]
internal class ExtraColorSchemes : IAffinity, IInitializable, IDisposable
{
    internal static bool IsSaving = false;
    internal static bool IsLoading = false;
    
    private PluginConfig _config;
    private ExtrasConfig _extrasConfig;
    private ColorScheme _firstColorScheme;
    
    public ExtraColorSchemes(PluginConfig config)
    {
        _config = config;
        _extrasConfig = config.Extras;
        _firstColorScheme = null!;
    }

    public void Initialize()
    {
        _config.PropertyChanged.AddListener(UpdateColorSchemes);
    }

    public void Dispose()
    {
        _config.PropertyChanged.RemoveListener(UpdateColorSchemes);
    }

    [AffinityPostfix]
    [AffinityPatch(typeof(ColorSchemesSettings), "", AffinityMethodType.Constructor,
        null, typeof(ColorScheme[]))]
    internal void cstr_ColorSchemesSettings(List<ColorScheme> ____colorSchemesList, string ____selectedColorSchemeId)
    {
        _firstColorScheme = ____colorSchemesList[0];

        if (_extrasConfig.SelectedColorSchemeId.Length == 0)
        {
            _extrasConfig.SelectedColorSchemeId = ____selectedColorSchemeId;
        }

        var isOutdated = _extrasConfig.ExtraColorSchemes.Any(x => x.NameLocalizationKey == "Default");

        if (_extrasConfig.ExtraColorSchemes.Count != 0 && !isOutdated) return;
        for (var i = 0; i < _extrasConfig.ExtraColorSchemeCount; i++)
        {
            XColorScheme? oldColorScheme = null;

            if (_extrasConfig.ExtraColorSchemes.Count > i)
                oldColorScheme = _extrasConfig.ExtraColorSchemes[i];

            var newColorScheme = oldColorScheme ?? new XColorScheme(____colorSchemesList[0]);
            newColorScheme.Id = $"User{i + 4}";
            newColorScheme.NameLocalizationKey = $"CUSTOM_{i + 4}_COLOR_SCHEME";

            if (oldColorScheme != null)
                _extrasConfig.ExtraColorSchemes.Remove(oldColorScheme);
            
            _extrasConfig.ExtraColorSchemes.Insert(i, newColorScheme);
        }
        
        _config.Changed();
    }
    
    [AffinityPostfix]
    [AffinityPatch(typeof(ColorSchemesSettings), nameof(ColorSchemesSettings.GetNumberOfColorSchemes))]
    internal void NumOfColorSchemes(ref int __result)
    {
        if (IsSaving) return;
        __result += _extrasConfig.ExtraColorSchemeCount;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(ColorSchemesSettings), nameof(ColorSchemesSettings.GetColorSchemeForIdx))]
    internal bool GetColorSchemeForIdx(ColorSchemesSettings __instance, ref ColorScheme __result, ref int idx)
    {
        if (IsSaving) return true;

        if (idx < 4)
            return true;
        if (idx >= _extrasConfig.ExtraColorSchemeCount + 4)
        {
            idx -= _extrasConfig.ExtraColorSchemeCount;
            return true;
        }

        __result = _extrasConfig.ExtraColorSchemes[idx - 4].ToColorScheme();
        return false;
    }

    [AffinityPostfix]
    [AffinityPatch(typeof(ColorSchemesSettings), nameof(ColorSchemesSettings.GetColorSchemeForId))]
    internal void GetColorSchemeForId(ref ColorScheme __result, string id)
    {
        var colorScheme = _extrasConfig.ExtraColorSchemes.FirstOrDefault(x => x.Id == id);
        if (colorScheme != null)
            __result = colorScheme.ToColorScheme();
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(ColorSchemesSettings), nameof(ColorSchemesSettings.GetSelectedColorScheme))]
    internal bool GetSelectedColorScheme(ref ColorScheme __result)
    {
        var colorScheme = _extrasConfig.ExtraColorSchemes.FirstOrDefault(x => x.Id == _extrasConfig.SelectedColorSchemeId);
        if (colorScheme == null) return true;
        __result = colorScheme.ToColorScheme();
        return false;
    }

    [AffinityPostfix]
    [AffinityPatch(typeof(ColorSchemesSettings), nameof(ColorSchemesSettings.GetSelectedColorSchemeIdx))]
    internal void GetSelectedColorSchemeIdx(ref int __result)
    {
        var idx = _extrasConfig.ExtraColorSchemes.FindIndex(x => x.Id == _extrasConfig.SelectedColorSchemeId);
        if (idx == -1 && __result >= 4)
        {
            __result += _extrasConfig.ExtraColorSchemeCount;
            return;
        }

        __result = idx + 4;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(ColorSchemesSettings), nameof(ColorSchemesSettings.SetColorSchemeForId))]
    internal void SetColorSchemeForId(ColorScheme colorScheme)
    {
        var idx = _extrasConfig.ExtraColorSchemes.FindIndex(x => x.Id == colorScheme.colorSchemeId);
        if (idx == -1) return;
        _extrasConfig.ExtraColorSchemes[idx] = new XColorScheme(colorScheme);
        _config.Changed();
    }

    [AffinityPostfix]
    [AffinityPatch(typeof(ColorSchemesSettings), nameof(ColorSchemesSettings.selectedColorSchemeId), AffinityMethodType.Getter)]
    internal void get_selectedColorSchemeId(ref string __result)
    {
        if (IsSaving) return;
        __result = _extrasConfig.SelectedColorSchemeId;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(ColorSchemesSettings), nameof(ColorSchemesSettings.selectedColorSchemeId), AffinityMethodType.Setter)]
    internal bool set_selectedColorSchemeId(string value, Dictionary<string, ColorScheme> ____colorSchemesDict)
    {
        var existsInCollection = _extrasConfig.ExtraColorSchemes.Any(x => x.Id == value);

        if (!existsInCollection && !____colorSchemesDict.ContainsKey(value))
            return false;

        if (IsLoading)
            return true;

        _extrasConfig.SelectedColorSchemeId = value;

        return !existsInCollection;
    }

    [AffinityTranspiler]
    [AffinityPatch(typeof(PlayerDataFileManagerSO), nameof(PlayerDataFileManagerSO.Save))]
    internal IEnumerable<CodeInstruction> MarkAsSaving(IEnumerable<CodeInstruction> instructions)
    {
        var res = instructions.ToList();
        var field = AccessTools.Field(typeof(ExtraColorSchemes), nameof(IsSaving));
        
        res.InsertRange(0, new[]
        {
            new CodeInstruction(OpCodes.Ldc_I4_1),
            new CodeInstruction(OpCodes.Stsfld, field)
        });
        
        res.InsertRange(res.Count - 1, new[]
        {
            new CodeInstruction(OpCodes.Ldc_I4_0),
            new CodeInstruction(OpCodes.Stsfld, field)
        });
        
        return res;
    }
    
    [AffinityTranspiler]
    [AffinityPatch(typeof(PlayerDataFileManagerSO), nameof(PlayerDataFileManagerSO.LoadFromCurrentVersion))]
    internal IEnumerable<CodeInstruction> MarkAsLoadingFromCurrentVersion(IEnumerable<CodeInstruction> instructions)
    {
        var res = instructions.ToList();
        var field = AccessTools.Field(typeof(ExtraColorSchemes), nameof(IsLoading));
        
        res.InsertRange(0, new[]
        {
            new CodeInstruction(OpCodes.Ldc_I4_1),
            new CodeInstruction(OpCodes.Stsfld, field)
        });
        
        res.InsertRange(res.Count - 1, new[]
        {
            new CodeInstruction(OpCodes.Ldc_I4_0),
            new CodeInstruction(OpCodes.Stsfld, field)
        });
        
        return res;
    }

    private void UpdateColorSchemes()
    {
        if (_extrasConfig.ExtraColorSchemes.Count > _extrasConfig.ExtraColorSchemeCount)
        {
            _extrasConfig.ExtraColorSchemes = _extrasConfig.ExtraColorSchemes.GetRange(0, _extrasConfig.ExtraColorSchemeCount);
        } else if (_extrasConfig.ExtraColorSchemes.Count < _extrasConfig.ExtraColorSchemeCount)
        {
            for (var i = _extrasConfig.ExtraColorSchemes.Count; i < _extrasConfig.ExtraColorSchemeCount; i++)
            {
                var colorScheme = new XColorScheme(_firstColorScheme)
                {
                    Id = $"User{i + 4}",
                    NameLocalizationKey = $"CUSTOM_{i + 4}_COLOR_SCHEME"
                };
                _extrasConfig.ExtraColorSchemes.Add(colorScheme);
            }
            _config.Changed();
        }
    }
}