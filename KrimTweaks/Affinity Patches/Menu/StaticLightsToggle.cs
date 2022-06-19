/*
MIT License

Copyright (c) 2021 Kinsi

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Collections;
using System.Linq;
using HMUI;
using KrimTweaks.Configuration;
using SiraUtil.Affinity;
using SiraUtil.Logging;
using UnityEngine;
using UnityEngine.UI;

namespace KrimTweaks.Affinity_Patches.Menu;

// ReSharper disable InconsistentNaming
internal class StaticLightsToggle : IAffinity
{
    private readonly SiraLog _siraLog;
    private readonly PluginConfig _config;
    
    private bool? _state = null;
    private PlayerSettingsPanelController _panelController;
    private EnvironmentEffectsFilterPresetDropdown _defaultDropdown;
    private EnvironmentEffectsFilterPresetDropdown _expertPlusDropdown;

    private GameObject replaceLabel;
    private ToggleWithCallbacks replaceToggle;

#pragma warning disable CS8618
    public StaticLightsToggle(SiraLog siraLog, PluginConfig config)
#pragma warning restore CS8618
    {
        _siraLog = siraLog;
        _config = config;
    }

    [AffinityPostfix]
    [AffinityPatch(typeof(PlayerSettingsPanelController), nameof(PlayerSettingsPanelController.SetLayout))]
    internal void Postfix(PlayerSettingsPanelController __instance, 
        EnvironmentEffectsFilterPresetDropdown ____environmentEffectsFilterDefaultPresetDropdown,
        EnvironmentEffectsFilterPresetDropdown ____environmentEffectsFilterExpertPlusPresetDropdown)
    {
        if (__instance.transform.parent.name == "PlayerSettingsViewController")
            return;
        
        _panelController = __instance;
        _defaultDropdown = ____environmentEffectsFilterDefaultPresetDropdown;
        _expertPlusDropdown = ____environmentEffectsFilterExpertPlusPresetDropdown;
        
        Setup(_config.Menu.UseStaticLightsToggle);
    }

    private void ToggleEffectState(bool state)
    {
        var effect = state ? EnvironmentEffectsFilterPreset.NoEffects : EnvironmentEffectsFilterPreset.AllEffects;
        
        _defaultDropdown.SelectCellWithValue(effect);
        _expertPlusDropdown.SelectCellWithValue(effect);
        
        _panelController.SetIsDirty();
    }

    private IEnumerator InitEffectState(bool state)
    {
        yield return new WaitForSeconds(.01f);
        
        ToggleEffectState(state);
    }

    private void Setup(bool enable)
    {
        if (_state == enable) 
            return;

        _state = enable;

        var container = _panelController.transform.Find("ViewPort/Content/CommonSection");

        container.GetComponent<VerticalLayoutGroup>().enabled = true;

        GameObject setActiveNext(EnvironmentEffectsFilterPresetDropdown dropdown, bool active)
        {
            var parent = dropdown.transform.parent;
            var sibling = parent.GetSiblingIndex();
            var next = parent.parent.GetChild(sibling + 1).gameObject;
            
            if (next.name == "-")
                next.SetActive(active);

            return dropdown.transform.parent.gameObject;
        }
        
        setActiveNext(_expertPlusDropdown, !enable).SetActive(!enable);

        var existingTableSettingRow = _defaultDropdown.transform.parent;
        var originalLabel = existingTableSettingRow.Find("Label").gameObject;

        if (replaceLabel == null)
        {
            replaceLabel = Object.Instantiate(originalLabel, existingTableSettingRow);
            Object.Destroy(replaceLabel.GetComponents<MonoBehaviour>().First(l => l.GetType().Name == "LocalizedTextMeshProUGUI"));
            replaceLabel.GetComponent<CurvedTextMeshPro>().text = "Static Lights";
        }

        if (replaceToggle == null)
        {
            replaceToggle = Object.Instantiate(container.GetComponentInChildren<ToggleWithCallbacks>(), existingTableSettingRow);
            replaceToggle.onValueChanged.RemoveAllListeners();
            replaceToggle.onValueChanged.AddListener(ToggleEffectState);
        }
        
        originalLabel.SetActive(!enable);
        existingTableSettingRow.Find("SimpleTextDropDown").gameObject.SetActive(!enable);
        replaceLabel.SetActive(enable);
        replaceToggle.gameObject.SetActive(enable);

        if (!enable) return;
        var targetState = _panelController.playerSpecificSettings.environmentEffectsFilterDefaultPreset ==
                          EnvironmentEffectsFilterPreset.NoEffects &&
                          _panelController.playerSpecificSettings.environmentEffectsFilterExpertPlusPreset ==
                          EnvironmentEffectsFilterPreset.NoEffects;

        replaceToggle.isOn = targetState;
        SharedCoroutineStarter.instance.StartCoroutine(InitEffectState(targetState));
    }

}