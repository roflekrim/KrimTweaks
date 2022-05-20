using System.Linq;
using UnityEngine;
using Zenject;

namespace KrimTweaks.Behaviours.Gameplay;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation
// ReSharper disable Unity.PerformanceCriticalCodeNullComparison
internal class MusicGroupLogoRemover : MonoBehaviour, IInitializable
{
    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    [InjectOptional] private AudioTimeSyncController _audioTimeSyncController = null!;

    public void Initialize()
    {
        if (_audioTimeSyncController == null)
            Destroy(gameObject);
    }
    
    public void Update()
    {
        if (_audioTimeSyncController.songTime == 0f)
            return;
        
        var gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        
        var go = gameObjects.FirstOrDefault(x => x.name == "MagicDoorSprite");
        if (go != null)
        {
            go.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
            return;
        }

        go = gameObjects.FirstOrDefault(x => x.name == "LinkinParkTextLogoL");
        if (go != null)
        {
            go.GetComponent<SpriteRenderer>().enabled = false;
            
            go = gameObjects.FirstOrDefault(x => x.name == "LinkinParkTextLogoR");
            if (go != null)
                go.GetComponent<SpriteRenderer>().enabled = false;
            
            go = gameObjects.FirstOrDefault(x => x.name == "Logo" && x.transform.parent.name == "Environment");
            if (go != null)
                go.GetComponent<SpriteRenderer>().enabled = false;
            
            Destroy(gameObject);
            return;
        }
        Destroy(gameObject);
    }
}