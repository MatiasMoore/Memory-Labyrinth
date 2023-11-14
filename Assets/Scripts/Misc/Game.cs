using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private void Start()
    {
        var audioController = FindObjectOfType<AudioController>();
        if (audioController == null)
            throw new System.Exception("No audio controller is found on startup");

        DontDestroyOnLoad(audioController.transform.parent.gameObject);

        var bonusStorage = FindObjectOfType<BonusStorage>();
        if (bonusStorage == null)
            throw new System.Exception("No BonusStorage is found on startup");
        bonusStorage.Init();

        var levelProgressStorage = FindObjectOfType<LevelProgressStorage>();
        if (levelProgressStorage == null)
            throw new System.Exception("No LevelProgressStorage is found on startup");
        levelProgressStorage.Init();

        var saveLoadManager = FindObjectOfType<SaveLoadManager>();
        if (saveLoadManager == null)
            throw new System.Exception("No SaveLoadManager is found on startup");

        DontDestroyOnLoad(saveLoadManager.transform.gameObject);

        saveLoadManager.LoadGame();

        ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);
    }
}
