﻿using System;
using System.Collections.Generic;
using KrimTweaks.Configuration;
using SiraUtil.Logging;
using UnityEngine;
using Zenject;

namespace KrimTweaks.Behaviours.Menu;

// ReSharper disable FieldCanBeMadeReadOnly.Local
internal class MenuNotes : MonoBehaviour, IInitializable, IDisposable
{
#pragma warning disable CS8618
    [Inject] private PluginConfig _config = null!;
#pragma warning restore CS8618
    
    private static readonly List<GameObject> DisabledNotes = new List<GameObject>();
    private static bool LastState = false;

    public void Initialize()
    {
        Handle();
        _config.PropertyChanged.AddListener(Handle);
    }

    public void Dispose()
    {
        _config.PropertyChanged.RemoveListener(Handle);
    }

    private void Start() => Handle();

    private void Handle()
    {
        if (LastState == _config.Menu.RemoveMenuNotes)
            return;

        LastState = _config.Menu.RemoveMenuNotes;
        
        if (_config.Menu.RemoveMenuNotes)
        {
            var environment = GameObject.Find("MenuEnvironmentManager/DefaultMenuEnvironment");
            DisabledNotes.Clear();
            DisabledNotes.AddRange(GetNotes(environment.transform));
            DisabledNotes.ForEach(go => go.SetActive(false));
        }
        else
        {
            DisabledNotes.ForEach(go => go.SetActive(true));
            DisabledNotes.Clear();
        }
    }

    private static IEnumerable<GameObject> GetNotes(Transform parent)
    {
        var notes = new List<GameObject>(); 
        foreach (Transform obj in parent)
        {
            if ((IsNote(obj) || IsNoteCollection(obj)) && obj.gameObject.activeSelf)
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

    private static bool IsNoteCollection(Transform transform)
    {
        var n = transform.name;
        return n == "PileOfNotes";
    }
}