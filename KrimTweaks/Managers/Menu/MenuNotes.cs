using System;
using System.Collections.Generic;
using System.Linq;
using KrimTweaks.Configuration;
using UnityEngine;
using Zenject;

namespace KrimTweaks.Managers.Menu;

// ReSharper disable FieldCanBeMadeReadOnly.Local
internal class MenuNotes : IInitializable, IDisposable
{
    private static List<GameObject> _disabledNotes = new();

    private PluginConfig _config;

    [Inject]
    public MenuNotes(PluginConfig config)
    {
        _config = config;
    }

    public void Initialize()
    {
        Handle();
        _config.PropertyChanged.AddListener(Handle);
    }

    public void Dispose()
    {
        _config.PropertyChanged.RemoveListener(Handle);
    }

    private void Handle()
    {
        if (_config.Menu.RemoveMenuNotes)
        {
            var environment = GameObject.Find("MenuEnvironmentManager/DefaultMenuEnvironment");
            _disabledNotes = _disabledNotes.Where(go => go != null).ToList();
            _disabledNotes.AddRange(GetNotes(environment.transform));
            _disabledNotes.ForEach(go => go.SetActive(false));
        }
        else
        {
            _disabledNotes = _disabledNotes.Where(go => go != null).ToList();
            _disabledNotes.ForEach(go => go.SetActive(true));
            _disabledNotes.Clear();
        }
    }

    private static IEnumerable<GameObject> GetNotes(Transform parent)
    {
        var notes = new List<GameObject>(); 
        foreach (Transform obj in parent)
        {
            if ((IsNote(obj) || obj.name == "PileOfNotes") && obj.gameObject.activeSelf)
            {
                notes.Add(obj.gameObject);
            }
            notes.AddRange(GetNotes(obj));
        }

        return notes;
    }
    
    private static bool IsNote(Transform transform)
    {
        var n = transform.name;
        return (n.Contains("Note (") || n.EndsWith("Note")) && !n.Contains("LevitatingNote");
    }
}