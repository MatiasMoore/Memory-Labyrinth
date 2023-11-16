using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField]
    private TouchControls _touchControls;

    [SerializeField]
    private MainCharacter _mainCharacter;

    [SerializeField]
    private Timer _timer;

    [SerializeField]
    private FogController _fogController;

    [SerializeField]
    private LevelModel _levelModel;

    [SerializeField]
    private LevelManager _levelManager;

    [SerializeField]
    private CameraScript _cameraScript;

    void Start()
    {
        _touchControls.Init();
        _mainCharacter.Init();
        _timer.Init();
        _fogController.Init();
        _levelModel.Init(_mainCharacter);
        _levelManager.Init();
        _cameraScript.Init();

        var audioController = FindObjectOfType<AudioController>();
        if (audioController != null)
            audioController.SetupListeners(_levelModel, _mainCharacter);
    }
}
