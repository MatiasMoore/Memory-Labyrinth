using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _levelPrefab;

    [SerializeField]
    static public ResourceManager.Level _currentLevel;

    private MainCharacter _mainCharacter;

    [SerializeField]
    private float _startLevelTime;

    private RightPathBuilder _rightPathBuilder;

    private float _timer;
    public void SetCurrentLevel(ResourceManager.Level level)
    {
        _currentLevel = level;
    }

    public void Start()
    {
        //place prefab on scene
        _levelPrefab = ResourceManager.LoadLevel(_currentLevel);
        Instantiate(_levelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        var audioController = FindObjectOfType<AudioController>();
        if (audioController != null )
            audioController.SetupListeners();
        
        LevelModel levelModel = FindObjectOfType<LevelModel>();
        _mainCharacter = FindObjectOfType<MainCharacter>();
        levelModel.Init(_mainCharacter);
        
        _rightPathBuilder = FindObjectOfType<RightPathBuilder>();
        _rightPathBuilder.ShowRightPath(_startLevelTime * 0.9f);
        _mainCharacter.Init();
        _mainCharacter.SetActive(false);
        
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _startLevelTime && _timer != 0)
        {
            _mainCharacter.SetActive(true);
            _rightPathBuilder.SetActive(false);
            //TODO: show fog
        }
        
    }
}
