using System;
using System.Linq;
using KrimTweaks.Configuration;
using UnityEngine;
using Zenject;

namespace KrimTweaks.Behaviours.VFX;

internal class WorldParticlesRemover : MonoBehaviour, IInitializable, IDisposable
{
    [Inject] private PluginConfig _config = null!;
    
    public void Initialize()
    {
        ToggleDust();
        _config.PropertyChanged.AddListener(ToggleDust);
    }

    public void Dispose()
    {
        _config.PropertyChanged.RemoveListener(ToggleDust);
    }

    public void ToggleDust()
    {
        Resources.FindObjectsOfTypeAll<GameObject>()
            .Where(go => go.name == "DustPS")
            .ToList()
            .ForEach(x => x.SetActive(!_config.VFX.DisableWorldParticles));
    }
}