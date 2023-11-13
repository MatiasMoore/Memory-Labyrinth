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

    [SerializeField]
    private GameObject _player;

    private MainCharacter _mainCharacter;

    [SerializeField]
    private LevelModel _levelModel;

    [SerializeField]
    private float _startLevelTime;

    private RightPathBuilder _rightPathBuilder;

    private float _timer;

    private bool _isLevelActive;

    public void SetCurrentLevel(ResourceManager.Level level)
    {
        _currentLevel = level;
    }

    public void Awake()
    {
        _player.SetActive(false);
        _mainCharacter = _player.GetComponent<MainCharacter>();

        var audioController = FindObjectOfType<AudioController>();
        if (audioController != null)
            audioController.SetupListeners();

        _levelModel.Init(_mainCharacter);
        Timer.SetTimerStatus(false);
        
    }

    public void Start()
    {
        if (!_isLevelActive)
        {
            _player.SetActive(true);
            
            //place prefab on scene
            _levelPrefab = ResourceManager.LoadLevel(_currentLevel);
            Instantiate(_levelPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            StartPoint startPoint = FindObjectOfType<StartPoint>();
            _player.GetComponent<Transform>().position = startPoint.GetPosition() + new Vector3(0,0,-1);
            

            _rightPathBuilder = FindObjectOfType<RightPathBuilder>();
            _rightPathBuilder.ShowRightPath(_startLevelTime * 0.9f);

            _mainCharacter.Init();
            _mainCharacter.SetActive(false);

            _timer = 0;
            _isLevelActive = true;
        }
    }

    private void Update()
    {
        if (_isLevelActive)
        {
            _timer += Time.deltaTime;

            if (_timer > _startLevelTime && _timer != 0)
            {
                _mainCharacter.SetActive(true);
                _rightPathBuilder.SetActive(false);
                Timer.SetTimerStatus(true);
                //TODO: show fog
            }
        }
    }
}
