using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private void Awake()
    {
        var audioController = FindObjectOfType<AudioController>();
        if (audioController == null)
            throw new System.Exception("No audio controller is found on startup");

        DontDestroyOnLoad(audioController.transform.parent.gameObject);

        var BonusStorage = FindObjectOfType<BonusStorage>();
        if (BonusStorage == null)
            throw new System.Exception("No BonusStorage is found on startup");

        DontDestroyOnLoad(BonusStorage.transform.parent.gameObject);

        var levelProgressStorage = FindObjectOfType<LevelProgressStorage>();
        if (levelProgressStorage == null)
            throw new System.Exception("No LevelProgressStorage is found on startup");

        DontDestroyOnLoad(levelProgressStorage.transform.parent.gameObject);

        ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);
    }
}
