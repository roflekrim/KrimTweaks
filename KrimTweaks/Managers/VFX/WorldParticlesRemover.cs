using System;
using System.Linq;
using KrimTweaks.Configuration;
using UnityEngine;
using Zenject;

namespace KrimTweaks.Managers.VFX;

internal class WorldParticlesRemover : IInitializable, IDisposable
{
    private PluginConfig _config;

    [Inject]
    public WorldParticlesRemover(PluginConfig config)
    {
        _config = config;
    }
    
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