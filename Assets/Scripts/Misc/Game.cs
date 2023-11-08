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

        audioController.Init();
        DontDestroyOnLoad(audioController.transform.parent.gameObject);

        ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);
    }
}
