using System;
using KrimTweaks.Configuration;
using UnityEngine;
using Zenject;

namespace KrimTweaks.Managers.Menu;

internal class AnniversaryRemover : IInitializable, IDisposable
{
    private const string AnniversaryWrapper =
        "Wrapper/MenuEnvironmentManager/DefaultMenuEnvironment/AnniversaryWrapper";

    private PluginConfig _config;

    [Inject]
    public AnniversaryRemover(PluginConfig config)
    {
        _config = config;
    }

    public void Initialize()
    {
        Update();
        _config.PropertyChanged.AddListener(Update);
    }

    public void Dispose()
    {
        _config.PropertyChanged.RemoveListener(Update);
    }

    private void Update()
    {
        if (GameObject.Find(AnniversaryWrapper) is not { } gameObject) return;
        gameObject.SetActive(!_config.Menu.DisableAnniversary);
    }
}